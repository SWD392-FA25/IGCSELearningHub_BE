using Application.Services.Interfaces;
using Application.Wrappers;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.DTOs.CoursePackages;

namespace Application.Services
{
    public class CoursePackageAdminService : ICoursePackageAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CoursePackageAdminService> _logger;

        public CoursePackageAdminService(IUnitOfWork uow, ILogger<CoursePackageAdminService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<PagedResult<PackageAdminListItemDTO>> GetListAsync(string? q, string? sort, int page, int pageSize)
        {
            var query = _uow.CoursePackageRepository.GetAllQueryable($"{nameof(CoursePackage.Courses)}");

            if (!string.IsNullOrWhiteSpace(q))
            {
                var key = q.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(key) ||
                                         (p.Description != null && p.Description.ToLower().Contains(key)));
            }

            // sort: createdAt_desc(default) | createdAt_asc | name_asc | name_desc | price_asc | price_desc
            query = (sort ?? "").ToLower() switch
            {
                "createdat_asc" => query.OrderBy(p => p.CreatedAt),
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderByDescending(p => p.CreatedAt)
            };

            return await query.ToPagedResultAsync(page, pageSize, p => new PackageAdminListItemDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
                CourseCount = p.Courses.Count(c => !c.IsDeleted)
            });
        }

        public async Task<Result<PackageAdminDetailDTO>> GetDetailAsync(int packageId)
        {
            var p = await _uow.CoursePackageRepository.GetAllQueryable($"{nameof(CoursePackage.Courses)}")
                .FirstOrDefaultAsync(x => x.Id == packageId);

            if (p == null) return Result<PackageAdminDetailDTO>.Fail("Package not found.", 404);

            var dto = new PackageAdminDetailDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
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

            return Result<PackageAdminDetailDTO>.Success(dto);
        }

        public async Task<Result<int>> CreateAsync(PackageCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return Result<int>.Fail("Name is required.", 400);
            if (dto.Price < 0)
                return Result<int>.Fail("Price must be >= 0.", 400);

            var package = new CoursePackage
            {
                Name = dto.Name.Trim(),
                Description = dto.Description,
                Price = dto.Price
            };

            await _uow.CoursePackageRepository.AddAsync(package);
            await _uow.SaveChangesAsync();

            // optional: attach courses ngay khi tạo
            if (dto.CourseIds != null && dto.CourseIds.Count > 0)
            {
                var courses = await _uow.CourseRepository
                    .GetAllQueryable()
                    .Where(c => dto.CourseIds.Contains(c.Id) && !c.IsDeleted)
                    .ToListAsync();

                foreach (var c in courses) package.Courses.Add(c);
                _uow.CoursePackageRepository.Update(package);
                await _uow.SaveChangesAsync();
            }

            return Result<int>.Success(package.Id, "Created", 201);
        }

        public async Task<Result<bool>> UpdateAsync(int packageId, PackageUpdateDTO dto)
        {
            var p = await _uow.CoursePackageRepository
                .GetAllQueryable($"{nameof(CoursePackage.Courses)}")
                .FirstOrDefaultAsync(x => x.Id == packageId);

            if (p == null) return Result<bool>.Fail("Package not found.", 404);

            if (string.IsNullOrWhiteSpace(dto.Name))
                return Result<bool>.Fail("Name is required.", 400);
            if (dto.Price < 0)
                return Result<bool>.Fail("Price must be >= 0.", 400);

            p.Name = dto.Name.Trim();
            p.Description = dto.Description;
            p.Price = dto.Price;

            if (dto.ReplaceAllCourses && dto.CourseIds != null)
            {
                // replace-all: clear current then add new set
                p.Courses.Clear();
                if (dto.CourseIds.Count > 0)
                {
                    var courses = await _uow.CourseRepository
                        .GetAllQueryable()
                        .Where(c => dto.CourseIds.Contains(c.Id) && !c.IsDeleted)
                        .ToListAsync();

                    foreach (var c in courses) p.Courses.Add(c);
                }
            }

            _uow.CoursePackageRepository.Update(p);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Updated", 200);
        }

        public async Task<Result<bool>> DeleteAsync(int packageId)
        {
            var p = await _uow.CoursePackageRepository.GetByIdAsync(packageId);
            if (p == null) return Result<bool>.Fail("Package not found.", 404);

            _uow.CoursePackageRepository.SoftDelete(p);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Deleted", 200);
        }

        public async Task<Result<bool>> AddCoursesAsync(int packageId, PackageAddCoursesDTO dto)
        {
            if (dto.CourseIds == null || dto.CourseIds.Count == 0)
                return Result<bool>.Fail("CourseIds cannot be empty.", 400);

            var p = await _uow.CoursePackageRepository
                .GetAllQueryable($"{nameof(CoursePackage.Courses)}")
                .FirstOrDefaultAsync(x => x.Id == packageId);

            if (p == null) return Result<bool>.Fail("Package not found.", 404);

            var existingIds = p.Courses.Select(c => c.Id).ToHashSet();
            var toAddIds = dto.CourseIds.Where(id => !existingIds.Contains(id)).ToList();

            if (toAddIds.Count == 0) return Result<bool>.Success(true, "No changes", 200);

            var courses = await _uow.CourseRepository
                .GetAllQueryable()
                .Where(c => toAddIds.Contains(c.Id) && !c.IsDeleted)
                .ToListAsync();

            foreach (var c in courses) p.Courses.Add(c);

            _uow.CoursePackageRepository.Update(p);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Courses added", 200);
        }

        public async Task<Result<bool>> RemoveCourseAsync(int packageId, int courseId)
        {
            var p = await _uow.CoursePackageRepository
                .GetAllQueryable($"{nameof(CoursePackage.Courses)}")
                .FirstOrDefaultAsync(x => x.Id == packageId);

            if (p == null) return Result<bool>.Fail("Package not found.", 404);

            var course = p.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null) return Result<bool>.Fail("Course not in package.", 404);

            p.Courses.Remove(course);
            _uow.CoursePackageRepository.Update(p);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Course removed", 200);
        }
    }
}
