using Application.IRepository;
using Application.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        private readonly AppDbContext _appDbContext;
        public UnitRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}
