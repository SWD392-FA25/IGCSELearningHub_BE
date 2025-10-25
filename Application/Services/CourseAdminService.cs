using Application.Services.Interfaces;
using Application.Wrappers;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.DTOs.Courses;

namespace Application.Services
{
    public class CourseAdminService : ICourseAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CourseAdminService> _logger;

        public CourseAdminService(IUnitOfWork uow, ILogger<CourseAdminService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<PagedResult<CourseAdminListItemDTO>> GetListAsync(
            string? q, string? level, int page, int pageSize, string? sort)
        {
            var query = _uow.CourseRepository.GetAllQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var key = q.Trim().ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(key) ||
                                         (c.Description != null && c.Description.ToLower().Contains(key)));
            }
            if (!string.IsNullOrWhiteSpace(level))
            {
                var lv = level.Trim();
                query = query.Where(c => c.Level == lv);
            }

            query = sort?.ToLower() switch
            {
                "price_asc" => query.OrderBy(c => c.Price),
                "price_desc" => query.OrderByDescending(c => c.Price),
                "title_desc" => query.OrderByDescending(c => c.Title),
                "title_asc" => query.OrderBy(c => c.Title),
                "createdat_asc" => query.OrderBy(c => c.CreatedAt),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };

            return await query.ToPagedResultAsync(page, pageSize, c => new CourseAdminListItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                Level = c.Level,
                Price = c.Price,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<Result<CourseAdminDetailDTO>> GetDetailAsync(int courseId)
        {
            var c = await _uow.CourseRepository.GetAllQueryable()
                .Include(x => x.Quizzes)
                .Include(x => x.Assignments)
                .Include(x => x.Livestreams)
                .Include(x => x.Enrollments)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (c == null) return Result<CourseAdminDetailDTO>.Fail("Course not found.", 404);

            var dto = new CourseAdminDetailDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Level = c.Level,
                Price = c.Price,
                CreatedAt = c.CreatedAt,
                TotalQuizzes = c.Quizzes.Count(x => !x.IsDeleted),
                TotalAssignments = c.Assignments.Count(x => !x.IsDeleted),
                TotalLivestreams = c.Livestreams.Count(x => !x.IsDeleted),
                TotalEnrollments = c.Enrollments.Count(x => !x.IsDeleted)
            };

            return Result<CourseAdminDetailDTO>.Success(dto);
        }

        public async Task<Result<int>> CreateAsync(CourseCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<int>.Fail("Title is required.", 400);
            if (dto.Price < 0)
                return Result<int>.Fail("Price must be >= 0.", 400);

            var course = new Course
            {
                Title = dto.Title.Trim(),
                Description = dto.Description,
                Level = dto.Level,
                Price = dto.Price
            };

            await _uow.CourseRepository.AddAsync(course);
            await _uow.SaveChangesAsync();

            return Result<int>.Success(course.Id, "Created", 201);
        }

        public async Task<Result<bool>> UpdateAsync(int courseId, CourseUpdateDTO dto)
        {
            var course = await _uow.CourseRepository.GetByIdAsync(courseId);
            if (course == null) return Result<bool>.Fail("Course not found.", 404);

            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<bool>.Fail("Title is required.", 400);
            if (dto.Price < 0)
                return Result<bool>.Fail("Price must be >= 0.", 400);

            course.Title = dto.Title.Trim();
            course.Description = dto.Description;
            course.Level = dto.Level;
            course.Price = dto.Price;

            _uow.CourseRepository.Update(course);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Updated", 200);
        }

        public async Task<Result<bool>> DeleteAsync(int courseId)
        {
            var course = await _uow.CourseRepository.GetByIdAsync(courseId);
            if (course == null) return Result<bool>.Fail("Course not found.", 404);

            _uow.CourseRepository.SoftDelete(course);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Deleted", 200);
        }
    }
}
