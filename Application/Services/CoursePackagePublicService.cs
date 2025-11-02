using Application.DTOs.CoursePackages;
using Application.Services.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Extensions;

namespace Application.Services
{
    public class CoursePackagePublicService : ICoursePackagePublicService
    {
        private readonly IUnitOfWork _uow;

        public CoursePackagePublicService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<PackagePublicListItemDTO>> GetListAsync(string? q, string? sort, int page, int pageSize)
        {
            var query = _uow.CoursePackageRepository.GetAllQueryable($"{nameof(CoursePackage.Courses)}");

            if (!string.IsNullOrWhiteSpace(q))
            {
                var key = q.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(key) ||
                                         (p.Description != null && p.Description.ToLower().Contains(key)));
            }

            query = (sort ?? "").ToLower() switch
            {
                "createdat_asc" => query.OrderBy(p => p.CreatedAt),
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderByDescending(p => p.CreatedAt)
            };

            return await query.ToPagedResultAsync(page, pageSize, p => new PackagePublicListItemDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CourseCount = p.Courses.Count(c => !c.IsDeleted)
            });
        }

        public async Task<Result<PackagePublicDetailDTO>> GetDetailAsync(int packageId)
        {
            var p = await _uow.CoursePackageRepository.GetAllQueryable($"{nameof(CoursePackage.Courses)}")
                .FirstOrDefaultAsync(x => x.Id == packageId);

            if (p == null) return Result<PackagePublicDetailDTO>.Fail("Package not found.", 404);

            var dto = new PackagePublicDetailDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Courses = p.Courses
                    .Where(c => !c.IsDeleted)
                    .Select(c => new PackageCourseItemDTO
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Level = c.Level,
                        Price = c.Price
                    }).ToList()
            };

            return Result<PackagePublicDetailDTO>.Success(dto);
        }
    }
}
