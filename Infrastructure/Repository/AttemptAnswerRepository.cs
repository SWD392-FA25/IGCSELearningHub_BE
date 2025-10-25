using Application.IRepository;
using Application.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class AttemptAnswerRepository : GenericRepository<AttemptAnswer>, IAttemptAnswerRepository
    {
        private readonly AppDbContext _appDbContext;
        public AttemptAnswerRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

