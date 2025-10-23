using Application.Authentication.Interfaces;
using Application.ViewModels.Authentication;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            IJwtTokenService jwtTokenService,
            ILogger<AuthenticationService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<AuthenticatedUserDTO>> RegisterAsync(AccountRegistrationDTO registrationDto)
        {
            if (string.IsNullOrWhiteSpace(registrationDto.Email) ||
                string.IsNullOrWhiteSpace(registrationDto.UserName) ||
                string.IsNullOrWhiteSpace(registrationDto.Password))
            {
                return Result<AuthenticatedUserDTO>.Fail("Invalid registration data.");
            }

            var accountExists = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(registrationDto.Email, registrationDto.UserName);

            if (accountExists != null)
            {
                _logger.LogWarning("User with email '{Email}' or username '{Username}' already exists", registrationDto.Email, registrationDto.UserName);
                return Result<AuthenticatedUserDTO>.Fail("User already exists.");
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
                try
                {
                    var account = _mapper.Map<Account>(registrationDto);
                    account.Password = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password, workFactor: 12);

                    await _unitOfWork.AccountRepository.AddAsync(account);
                    await _unitOfWork.SaveChangesAsync();

                    var tokenResult = _jwtTokenService.GenerateJwtToken(
                        account.Id,
                        account.UserName,
                        account.Role,
                        account.Status
                    );

                    if (!tokenResult.Succeeded)
                    {
                        return Result<AuthenticatedUserDTO>.Fail(tokenResult.Message ?? "Failed to generate token.");
                    }

                    await transaction.CommitAsync();

                    var response = new AuthenticatedUserDTO
                    {
                        JWTToken = tokenResult.Data!,
                        Id = account.Id,
                        UserName = account.UserName,
                        FullName = account.FullName,
                        Email = account.Email,
                        Role = account.Role.ToString(),
                        Status = account.Status,
                        IsExternal = account.IsExternal
                    };

                    return Result<AuthenticatedUserDTO>.Success(response);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return Result<AuthenticatedUserDTO>.Fail("Email or username already in use.", 409);
                }
        }

        public async Task<Result<AuthenticatedUserDTO>> LoginAsync(AccountLoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.EmailOrUsername) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return Result<AuthenticatedUserDTO>.Fail("Invalid login data.");
            }

            var emailOrUsername = loginDto.EmailOrUsername.Trim();

            var account = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(emailOrUsername, emailOrUsername);

            if (account == null || !string.Equals(account.Status, "Active", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("Invalid credentials or account is banned.");
                return Result<AuthenticatedUserDTO>.Fail("Invalid credentials or account is banned.");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, account.Password))
            {
                _logger.LogWarning("Invalid credentials provided.");
                return Result<AuthenticatedUserDTO>.Fail("Invalid credentials.");
            }

            var tokenResult = _jwtTokenService.GenerateJwtToken(account.Id, account.UserName, account.Role, account.Status);

            if (!tokenResult.Succeeded)
            {
                return Result<AuthenticatedUserDTO>.Fail(tokenResult.Message ?? "Failed to generate token.");
            }

            _logger.LogInformation("User login successful for AccountId: {AccountId}", account.Id);

            var response = new AuthenticatedUserDTO
            {
                JWTToken = tokenResult.Data!,
                Id = account.Id,
                UserName = account.UserName,
                FullName = account.FullName,
                Email = account.Email,
                Role = account.Role.ToString(),
                Status = account.Status,
                IsExternal = account.IsExternal
            };

            return Result<AuthenticatedUserDTO>.Success(response);
        }
    }
}
