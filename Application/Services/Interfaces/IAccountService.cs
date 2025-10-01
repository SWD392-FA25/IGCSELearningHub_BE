using Application.ViewModels;
using Application.ViewModels.Accounts;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Result<AccountDTO?>> GetAccountByIdAsync(int id);
        Task<Result<IEnumerable<AccountDTO>>> GetAllAccountsAsync();
        Task<Result<PaginatedList<AccountDTO>>> GetAllAccountsPaginatedAsync(QueryParameters queryParameters);
        Task<Result<bool>> CheckUsernameOrEmailExistsAsync(string email, string username);
        Task<Result<AccountDTO?>> UpdateAccountAsync(int accountId, UpdateAccountDTO updateDto);
        Task<Result<string>> ResetPasswordAsync(ResetPasswordDTO dto);
        Task<Result<string>> BanAccountAsync(int accountId);
        Task<Result<string>> UnbanAccountAsync(int accountId);
    }
}
