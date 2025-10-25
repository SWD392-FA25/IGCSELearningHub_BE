using Application.DTOs.Courses;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICourseCatalogService
    {
        Task<PagedResult<CourseCatalogItemDTO>> GetCatalogAsync(
            string? q, string? level, decimal? priceMin, decimal? priceMax,
            int page, int pageSize, string? sort);

        Task<Result<CourseDetailDTO>> GetDetailAsync(int courseId);
    }
}
