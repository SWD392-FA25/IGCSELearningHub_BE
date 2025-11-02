using Application.DTOs.Livestreams;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface ILivestreamPublicService
    {
        Task<PagedResult<LivestreamPublicListItemDTO>> GetUpcomingListAsync(int? courseId, DateTime? from, DateTime? to, string? sort, int page, int pageSize);
        Task<Result<LivestreamPublicDetailDTO>> GetDetailAsync(int livestreamId);
    }
}

