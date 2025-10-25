using Application.DTOs.Accounts;

using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Result<AccountDTO?>> GetAccountByIdAsync(int id);
        Task<Result<IEnumerable<AccountDTO>>> GetAllAccountsAsync();
        Task<PagedResult<AccountDTO>> GetAccountsPagedAsync(string? q, string? role, string? status, int page, int pageSize, string? sort);
        Task<Result<bool>> CheckUsernameOrEmailExistsAsync(string username, string email);
        Task<Result<AccountDTO?>> UpdateAccountAsync(int accountId, UpdateAccountDTO updateDto);
        Task<Result<string>> ResetPasswordAsync(ResetPasswordDTO dto);
        Task<Result<string>> BanAccountAsync(int accountId);
        Task<Result<string>> UnbanAccountAsync(int accountId);
    }
}
