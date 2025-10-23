using Application.ViewModels.Courses;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICourseAdminService
    {
        Task<PagedResult<CourseAdminListItemDTO>> GetListAsync(
            string? q, string? level, int page, int pageSize, string? sort);

        Task<Result<CourseAdminDetailDTO>> GetDetailAsync(int courseId);
        Task<Result<int>> CreateAsync(CourseCreateDTO dto);       // return new Id
        Task<Result<bool>> UpdateAsync(int courseId, CourseUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int courseId);
    }
}
