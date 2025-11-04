using Application.IRepository;
using Application.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class LessonCompletionRepository : GenericRepository<LessonCompletion>, ILessonCompletionRepository
    {
        private readonly AppDbContext _appDbContext;
        public LessonCompletionRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

