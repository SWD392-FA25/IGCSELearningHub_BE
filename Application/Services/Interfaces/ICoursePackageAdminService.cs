using Application.DTOs.CoursePackages;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICoursePackageAdminService
    {
        Task<PagedResult<PackageAdminListItemDTO>> GetListAsync(string? q, string? sort, int page, int pageSize);
        Task<Result<PackageAdminDetailDTO>> GetDetailAsync(int packageId);
        Task<Result<int>> CreateAsync(PackageCreateDTO dto);          // return new PackageId
        Task<Result<bool>> UpdateAsync(int packageId, PackageUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int packageId);

        Task<Result<bool>> AddCoursesAsync(int packageId, PackageAddCoursesDTO dto);
        Task<Result<bool>> RemoveCourseAsync(int packageId, int courseId);
    }
}
