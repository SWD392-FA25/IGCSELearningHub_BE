using Application.IRepository;
using Application.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _db;
        public RefreshTokenRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _db = dbContext;
        }
    }
}

