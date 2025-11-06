using Application.IRepository;
using Application.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class CurriculumRepository : GenericRepository<Curriculum>, ICurriculumRepository
    {
        private readonly AppDbContext _appDbContext;
        public CurriculumRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}
