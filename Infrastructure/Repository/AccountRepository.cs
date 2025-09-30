using Application.IRepository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext _appDbContext;
        public AccountRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }


        public async Task<Account> GetByUsernameOrEmail(string email, string username)
        {
            var account = await _appDbContext.Accounts.FirstOrDefaultAsync(x => x.Email == email || x.UserName == username);

            return account;
        }

        public async Task<bool> AnyAsync(Expression<Func<Account, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
