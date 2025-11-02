using Application.Authentication.Interfaces;
using Application.DTOs.Authentication;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        private const int RefreshTokenDays = 7;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            ILogger<AuthenticationService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
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

                    await transaction.CommitAsync();
                    return await _tokenService.IssueAsync(account);
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

            _logger.LogInformation("User login successful for AccountId: {AccountId}", account.Id);
            return await _tokenService.IssueAsync(account);
        }

        public async Task<Result<AuthenticatedUserDTO>> RefreshAsync(RefreshTokenRequestDTO request, string? ipAddress = null)
            => await _tokenService.RefreshAsync(request.RefreshToken, ipAddress);
    }
}
