using Application.Services.Interfaces;
using Application.Wrappers;
using Application.Extensions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Application.DTOs.Accounts;
using Application.Utils.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public AccountService(
            IUnitOfWork unitOfWork,
            ILogger<AccountService> logger,
            IMapper mapper,
            IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<Result<AccountDTO>> GetAccountByIdAsync(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (account == null) return Result<AccountDTO>.Fail("Account not found.", 404);

            return Result<AccountDTO>.Success(_mapper.Map<AccountDTO>(account), "Account retrieved successfully.");
        }

        public async Task<Result<IEnumerable<AccountDTO>>> GetAllAccountsAsync()
        {
            var allAccounts = await _unitOfWork.AccountRepository.GetAllAsync();
            return Result<IEnumerable<AccountDTO>>.Success(_mapper.Map<IEnumerable<AccountDTO>>(allAccounts), "All accounts retrieved successfully.");
        }

        public async Task<PagedResult<AccountDTO>> GetAccountsPagedAsync(string? q, string? role, string? status, int page, int pageSize, string? sort)
        {
            var query = _unitOfWork.AccountRepository.GetAllQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var key = q.Trim().ToLower();
                query = query.Where(a =>
                    a.UserName.ToLower().Contains(key) ||
                    a.Email.ToLower().Contains(key) ||
                    (a.FullName != null && a.FullName.ToLower().Contains(key)));
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                var r = role.Trim();
                query = query.Where(a => a.Role.ToString() == r);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                var s = status.Trim().ToLower();
                query = query.Where(a => (a.Status ?? "").ToLower() == s);
            }

            query = (sort ?? "").ToLower() switch
            {
                "createdat_asc" => query.OrderBy(a => a.CreatedAt),
                "name_asc" => query.OrderBy(a => a.UserName),
                "name_desc" => query.OrderByDescending(a => a.UserName),
                "email_asc" => query.OrderBy(a => a.Email),
                "email_desc" => query.OrderByDescending(a => a.Email),
                _ => query.OrderByDescending(a => a.CreatedAt)
            };

            return await query.ToPagedResultAsync(page, pageSize, a => _mapper.Map<AccountDTO>(a));
        }

        public async Task<Result<bool>> CheckUsernameOrEmailExistsAsync(string username, string email)
        {
            var existedAccount = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(email, username);
            return Result<bool>.Success(existedAccount != null,
                existedAccount != null ? "Username or Email already exists." : "Username or Email does not exist.");
        }

        public async Task<Result<AccountDTO>> UpdateAccountAsync(int accountId, UpdateAccountDTO updateDto)
        {
            var existedAccount = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(updateDto.Email, updateDto.UserName);

            if (existedAccount != null && existedAccount.Id != accountId)
            {
                return Result<AccountDTO>.Fail("User already exists with the provided email or username.", 400);
            }

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if (account == null) return Result<AccountDTO>.Fail("Account not found.", 404);

            account.FullName = updateDto.FullName;
            account.PhoneNumber = updateDto.PhoneNumber;
            account.UserName = updateDto.UserName;
            account.Email = updateDto.Email;

            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return Result<AccountDTO>.Success(_mapper.Map<AccountDTO>(account), "Account updated successfully.");
        }

        public async Task<Result<string>> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var account = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(dto.UserNameOrEmail, dto.UserNameOrEmail);
            if (account == null)
                return Result<string>.Fail("Account with the given email or username does not exist.", 400);

            if (account.IsExternal && !string.IsNullOrWhiteSpace(account.ExternalProvider))
                return Result<string>.Fail("Tài khoản này chỉ có thể đăng nhập bằng Google.", 400);

            if (dto.NewPassword != dto.ConfirmNewPassword)
                return Result<string>.Fail("New password and confirmation do not match.", 400);

            account.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword, workFactor: 12);

            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Password reset successfully.");
        }

        public async Task<Result<string>> SendPasswordResetEmailAsync(ForgotPasswordRequestDTO dto, string origin, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return Result<string>.Fail("Email is required.", 400);

            var account = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(dto.Email, dto.Email);
            if (account == null)
            {
                return Result<string>.Success(GenericPasswordResetResponse);
            }

            if (account.IsExternal && !string.IsNullOrWhiteSpace(account.ExternalProvider))
            {
                return Result<string>.Fail("Tài khoản này chỉ có thể đăng nhập bằng Google.", 400);
            }

            var originalPassword = account.Password;
            var temporaryPassword = GenerateTemporaryPassword();
            account.Password = BCrypt.Net.BCrypt.HashPassword(temporaryPassword, workFactor: 12);

            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            var subject = "IGCSE Learning Hub - Password Reset";
            var body = BuildResetEmailBody(account.FullName, account.UserName, temporaryPassword, BuildResetUrl(origin));

            try
            {
                await _emailSender.SendAsync(account.Email, subject, body, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send password reset email to {Email}", account.Email);
                account.Password = originalPassword;
                _unitOfWork.AccountRepository.Update(account);
                await _unitOfWork.SaveChangesAsync();
                return Result<string>.Fail("Failed to send password reset email. Please try again later.", 500);
            }

            return Result<string>.Success(GenericPasswordResetResponse);
        }

        private const string GenericPasswordResetResponse = "If an account exists for the provided email, a reset email has been sent.";

        private static string GenerateTemporaryPassword(int length = 12)
        {
            const string allowed = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789@$!%*?&";
            var bytes = RandomNumberGenerator.GetBytes(length);
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowed[bytes[i] % allowed.Length];
            }
            return new string(chars);
        }

        private static string BuildResetUrl(string origin)
        {
            if (string.IsNullOrWhiteSpace(origin)) return string.Empty;
            return $"{origin.TrimEnd('/')}/reset-password";
        }

        private static string BuildResetEmailBody(string? fullName, string userName, string tempPassword, string resetUrl)
        {
            var displayName = string.IsNullOrWhiteSpace(fullName) ? userName : fullName;
            var sb = new StringBuilder();
            sb.AppendLine($"<p>Hi {displayName},</p>");
            sb.AppendLine("<p>We received a request to reset your password. Use the temporary password below to sign in and change it immediately:</p>");
            sb.AppendLine($"<p><strong>Temporary password:</strong> {tempPassword}</p>");
            sb.AppendLine("<p>After signing in, please update your password from your profile page.</p>");
            if (!string.IsNullOrWhiteSpace(resetUrl))
            {
                sb.AppendLine($"<p>You can access the reset page here: <a href=\"{resetUrl}\">{resetUrl}</a></p>");
            }
            sb.AppendLine("<p>If you didn’t request a reset, you can ignore this email. Your password will remain unchanged, but we recommend updating it if you have any concerns.</p>");
            sb.AppendLine("<p>Best regards,<br/>IGCSE Learning Hub Team</p>");
            return sb.ToString();
        }

        public async Task<Result<string>> BanAccountAsync(int accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if (account == null) return Result<string>.Fail("Account not found.", 404);

            account.Status = "Banned";
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Account banned successfully.");
        }

        public async Task<Result<string>> UnbanAccountAsync(int accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if (account == null) return Result<string>.Fail("Account not found.", 404);

            account.Status = "Active";
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Account unbanned successfully.");
        }
    }
}
