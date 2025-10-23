using Application.Services.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Accounts;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;

        public AccountService(
            IUnitOfWork unitOfWork,
            ILogger<AccountService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
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
            page = page < 1 ? 1 : page;
            pageSize = pageSize is <= 0 or > 100 ? 20 : pageSize;

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

            // sort: createdAt_desc (default) | createdAt_asc | name_asc|name_desc | email_asc|email_desc
            query = (sort ?? "").ToLower() switch
            {
                "createdat_asc" => query.OrderBy(a => a.CreatedAt),
                "name_asc" => query.OrderBy(a => a.UserName),
                "name_desc" => query.OrderByDescending(a => a.UserName),
                "email_asc" => query.OrderBy(a => a.Email),
                "email_desc" => query.OrderByDescending(a => a.Email),
                _ => query.OrderByDescending(a => a.CreatedAt)
            };

            var total = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => _mapper.Map<AccountDTO>(a))
                .ToListAsync();

            return PagedResult<AccountDTO>.Success(data, total, page, pageSize);
        }

        public async Task<Result<bool>> CheckUsernameOrEmailExistsAsync(string username, string email)
        {
            // sửa lại thứ tự đúng (username, email)
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

            if (dto.NewPassword != dto.ConfirmNewPassword)
                return Result<string>.Fail("New password and confirmation do not match.", 400);

            account.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword, workFactor: 12);

            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Password reset successfully.");
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
