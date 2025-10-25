using Application.IRepository;
using Application.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class LivestreamRegistrationRepository : GenericRepository<LivestreamRegistration>, ILivestreamRegistrationRepository
    {
        private readonly AppDbContext _appDbContext;
        public LivestreamRegistrationRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

