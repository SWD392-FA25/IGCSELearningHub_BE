using Application.DTOs.CoursePackages;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface ICoursePackagePublicService
    {
        Task<PagedResult<PackagePublicListItemDTO>> GetListAsync(string? q, string? sort, int page, int pageSize);
        Task<Result<PackagePublicDetailDTO>> GetDetailAsync(int packageId);
    }
}

