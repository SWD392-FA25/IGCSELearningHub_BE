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
        private readonly IUnitOfWork _uow;

        public TokenService(IAccessTokenFactory access, IRefreshTokenManager refresh, IUnitOfWork uow)
        {
            _access = access;
            _refresh = refresh;
            _uow = uow;
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
            if (!ok || token == null)
                return Result<AuthenticatedUserDTO>.Fail(error ?? "Invalid refresh token.", 401);

            var account = await _uow.AccountRepository.GetByIdAsync(token.AccountId);
            if (account == null)
                return Result<AuthenticatedUserDTO>.Fail("Account not found for this token.", 401);

            var rotate = await _refresh.RotateAsync(refreshToken, ipAddress);
            if (!rotate.Ok || rotate.NewToken == null)
                return Result<AuthenticatedUserDTO>.Fail(rotate.Error ?? "Unable to rotate refresh token.", 401);

            var access = _access.GenerateAccessToken(account.Id, account.UserName, account.Role, account.Status);
            if (!access.Succeeded)
                return Result<AuthenticatedUserDTO>.Fail(access.Message ?? "Failed to create access token.");

            var dto = new AuthenticatedUserDTO
            {
                AccessToken = access.Data!,
                RefreshToken = rotate.NewToken.Token,
                Id = account.Id,
                UserName = account.UserName,
                FullName = account.FullName ?? string.Empty,
                Email = account.Email,
                Role = account.Role.ToString(),
                Status = account.Status ?? string.Empty,
                IsExternal = account.IsExternal
            };
            return Result<AuthenticatedUserDTO>.Success(dto);
        }

        public async Task<bool> RevokeByTokenAsync(string refreshToken, string? reason, string? ipAddress = null)
            => await _refresh.RevokeByTokenAsync(refreshToken, reason, ipAddress);

        public async Task<int> RevokeAllForAccountAsync(int accountId, string? reason, string? ipAddress = null)
            => await _refresh.RevokeAllForAccountAsync(accountId, reason, ipAddress);
    }
}
