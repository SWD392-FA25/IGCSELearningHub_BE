using Application.DTOs.Authentication;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Authentication.Interfaces
{
    public interface ITokenService
    {
        Task<Result<AuthenticatedUserDTO>> IssueAsync(Account account, string? ipAddress = null);
        Task<Result<AuthenticatedUserDTO>> RefreshAsync(string refreshToken, string? ipAddress = null);
        Task<bool> RevokeByTokenAsync(string refreshToken, string? reason, string? ipAddress = null);
        Task<int> RevokeAllForAccountAsync(int accountId, string? reason, string? ipAddress = null);
    }
}

