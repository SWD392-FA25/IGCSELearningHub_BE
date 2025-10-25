using Application.IRepository;
using Application.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class LivestreamRepository : GenericRepository<Livestream>, ILivestreamRepository
    {
        private readonly AppDbContext _appDbContext;
        public LivestreamRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

