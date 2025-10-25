using Application.Services.Interfaces;
using Application.ViewModels.Courses;
using Application.Wrappers;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CourseCatalogService : ICourseCatalogService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CourseCatalogService> _logger;

        public CourseCatalogService(IUnitOfWork uow, ILogger<CourseCatalogService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<PagedResult<CourseCatalogItemDTO>> GetCatalogAsync(
            string? q, string? level, decimal? priceMin, decimal? priceMax,
            int page, int pageSize, string? sort)
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
            if (priceMin.HasValue) query = query.Where(c => c.Price >= priceMin.Value);
            if (priceMax.HasValue) query = query.Where(c => c.Price <= priceMax.Value);

            // sort: "createdAt_desc|createdAt_asc|price_asc|price_desc|title_asc|title_desc"
            query = sort?.ToLower() switch
            {
                "price_asc" => query.OrderBy(c => c.Price),
                "price_desc" => query.OrderByDescending(c => c.Price),
                "title_desc" => query.OrderByDescending(c => c.Title),
                "title_asc" => query.OrderBy(c => c.Title),
                "createdat_asc" => query.OrderBy(c => c.CreatedAt),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };

            // Include counts via navigation collections using subqueries (efficient)
            return await query.ToPagedResultAsync(page, pageSize, c => new CourseCatalogItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                Level = c.Level,
                Price = c.Price,
                ShortDescription = c.Description != null && c.Description.Length > 160
                    ? c.Description.Substring(0, 160) + "..."
                    : c.Description,
                TotalQuizzes = c.Quizzes.Count(x => !x.IsDeleted),
                TotalAssignments = c.Assignments.Count(x => !x.IsDeleted)
            });
        }

        public async Task<Result<CourseDetailDTO>> GetDetailAsync(int courseId)
        {
            var course = await _uow.CourseRepository.GetAllQueryable()
                .Include(c => c.Quizzes)
                .Include(c => c.Assignments)
                .Include(c => c.Livestreams)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null) return Result<CourseDetailDTO>.Fail("Course not found.", 404);

            var dto = new CourseDetailDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Level = course.Level,
                Price = course.Price,
                TotalQuizzes = course.Quizzes.Count(x => !x.IsDeleted),
                TotalAssignments = course.Assignments.Count(x => !x.IsDeleted),
                TotalLivestreams = course.Livestreams.Count(x => !x.IsDeleted)
            };

            return Result<CourseDetailDTO>.Success(dto);
        }
    }
}
