using Domain.Entities;

namespace Application.Authentication.Interfaces
{
    public interface IRefreshTokenManager
    {
        Task<RefreshToken> CreateAsync(int accountId, string? ipAddress);
        Task<(bool Ok, RefreshToken? Token, string? Error)> ValidateActiveAsync(string token);
        Task<(bool Ok, RefreshToken? NewToken, string? Error)> RotateAsync(string token, string? ipAddress);
        Task<bool> RevokeByTokenAsync(string token, string? reason, string? ipAddress);
        Task<int> RevokeAllForAccountAsync(int accountId, string? reason, string? ipAddress);
    }
}

