using Application.IRepository;
using Application.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class CoursePackageRepository : GenericRepository<CoursePackage>, ICoursePackageRepository
    {
        private readonly AppDbContext _appDbContext;
        public CoursePackageRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

