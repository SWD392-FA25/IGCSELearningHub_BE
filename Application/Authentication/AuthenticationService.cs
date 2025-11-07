using System;
using System.Threading;
using Application.Authentication.Interfaces;
using Application.DTOs.Authentication;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        private readonly IExternalAuthProvider _externalAuth;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            ILogger<AuthenticationService> logger,
            IMapper mapper,
            IExternalAuthProvider externalAuth)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
            _externalAuth = externalAuth;
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

        public async Task<Result<AuthenticatedUserDTO>> LoginWithFirebaseAsync(FirebaseLoginRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.IdToken))
                return Result<AuthenticatedUserDTO>.Fail("Firebase id token is required.");

            try
            {
                var externalUser = await _externalAuth.ValidateTokenAsync(dto.IdToken);

                if (string.IsNullOrWhiteSpace(externalUser.Email) || !externalUser.EmailVerified)
                    return Result<AuthenticatedUserDTO>.Fail("External account must include a verified email.");

                var account = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(externalUser.Email, externalUser.Email);
                if (account == null)
                {
                    account = await CreateExternalAccountAsync(externalUser);
                }
                else if (!string.Equals(account.Status, "Active", StringComparison.OrdinalIgnoreCase))
                {
                    return Result<AuthenticatedUserDTO>.Fail("Account is banned or inactive.");
                }
                else if (!account.IsExternal)
                {
                    return Result<AuthenticatedUserDTO>.Fail("Tài khoản này đăng ký bằng Email/Password. Vui lòng đăng nhập bằng mật khẩu.", 400);
                }
                else if (!string.Equals(account.ExternalProvider, _externalAuth.ProviderName, StringComparison.OrdinalIgnoreCase))
                {
                    return Result<AuthenticatedUserDTO>.Fail("Tài khoản này được tạo bằng phương thức khác. Vui lòng đăng nhập bằng phương thức phù hợp.", 400);
                }

                return await _tokenService.IssueAsync(account);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "External auth failed.");
                return Result<AuthenticatedUserDTO>.Fail("Invalid external token.");
            }
        }

        private async Task<Account> CreateExternalAccountAsync(ExternalAuthUser externalUser)
        {
            var baseUserName = !string.IsNullOrWhiteSpace(externalUser.PreferredUserName)
                ? externalUser.PreferredUserName!
                : externalUser.Email.Split('@')[0];

            var uniqueUserName = await GenerateUniqueUsernameAsync(baseUserName);

            var account = new Account
            {
                Email = externalUser.Email,
                UserName = uniqueUserName,
                FullName = externalUser.FullName,
                Status = "Active",
                IsExternal = true,
                ExternalProvider = _externalAuth.ProviderName,
                ExternalId = externalUser.ExternalId
            };

            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Created new {Provider} account for {Email} (AccountId: {AccountId})", _externalAuth.ProviderName, externalUser.Email, account.Id);
            return account;
        }

        private async Task<string> GenerateUniqueUsernameAsync(string baseUserName)
        {
            var normalized = NormalizeUserName(baseUserName);
            var candidate = normalized;
            var suffix = 1;
            while (await _unitOfWork.AccountRepository.AnyAsync(a => a.UserName == candidate))
            {
                candidate = $"{normalized}{suffix++}";
            }
            return candidate;
        }

        private static string NormalizeUserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return $"user{Guid.NewGuid():N}".Substring(0, 10);

            var normalized = Regex.Replace(value.ToLowerInvariant(), "[^a-z0-9]", string.Empty);
            if (string.IsNullOrWhiteSpace(normalized))
            {
                normalized = $"user{Guid.NewGuid():N}".Substring(0, 10);
            }

            return normalized.Length > 20 ? normalized[..20] : normalized;
        }
    }
}
