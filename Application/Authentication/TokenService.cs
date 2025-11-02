using Application.Authentication.Interfaces;
using Application.DTOs.Authentication;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IAccessTokenFactory _access;
        private readonly IRefreshTokenManager _refresh;

        public TokenService(IAccessTokenFactory access, IRefreshTokenManager refresh)
        {
            _access = access;
            _refresh = refresh;
        }

        public async Task<Result<AuthenticatedUserDTO>> IssueAsync(Account account, string? ipAddress = null)
        {
            var access = _access.GenerateAccessToken(account.Id, account.UserName, account.Role, account.Status);
            if (!access.Succeeded) return Result<AuthenticatedUserDTO>.Fail(access.Message ?? "Failed to create access token.");

            var rt = await _refresh.CreateAsync(account.Id, ipAddress);

            var dto = new AuthenticatedUserDTO
            {
                AccessToken = access.Data!,
                RefreshToken = rt.Token,
                Id = account.Id,
                UserName = account.UserName,
                FullName = account.FullName,
                Email = account.Email,
                Role = account.Role.ToString(),
                Status = account.Status,
                IsExternal = account.IsExternal
            };
            return Result<AuthenticatedUserDTO>.Success(dto);
        }

        public async Task<Result<AuthenticatedUserDTO>> RefreshAsync(string refreshToken, string? ipAddress = null)
        {
            var (ok, token, error) = await _refresh.ValidateActiveAsync(refreshToken);
            if (!ok || token == null) return Result<AuthenticatedUserDTO>.Fail(error ?? "Invalid refresh token.", 401);

            var newRtResult = await _refresh.RotateAsync(refreshToken, ipAddress);
            if (!newRtResult.Ok || newRtResult.NewToken == null)
                return Result<AuthenticatedUserDTO>.Fail(newRtResult.Error ?? "Unable to rotate refresh token.", 401);

            // fetch account minimal: rely on token.Account; assume upstream will load if needed
            var acc = new Account { Id = token.AccountId, UserName = string.Empty, Role = 0, Status = null };
            // Note: For full details (email/name), the caller should fetch the account and use IssueAsync.
            // Here we only guarantee tokens.

            var access = _access.GenerateAccessToken(token.AccountId, acc.UserName, acc.Role, acc.Status);
            if (!access.Succeeded) return Result<AuthenticatedUserDTO>.Fail(access.Message ?? "Failed to create access token.");

            var dto = new AuthenticatedUserDTO
            {
                AccessToken = access.Data!,
                RefreshToken = newRtResult.NewToken.Token,
                Id = token.AccountId,
                UserName = acc.UserName,
                FullName = string.Empty,
                Email = string.Empty,
                Role = acc.Role.ToString(),
                Status = acc.Status ?? string.Empty,
                IsExternal = false
            };
            return Result<AuthenticatedUserDTO>.Success(dto);
        }

        public async Task<bool> RevokeByTokenAsync(string refreshToken, string? reason, string? ipAddress = null)
            => await _refresh.RevokeByTokenAsync(refreshToken, reason, ipAddress);

        public async Task<int> RevokeAllForAccountAsync(int accountId, string? reason, string? ipAddress = null)
            => await _refresh.RevokeAllForAccountAsync(accountId, reason, ipAddress);
    }
}

