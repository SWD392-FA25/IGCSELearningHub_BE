using Application.DTOs.Livestreams;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface ILivestreamAdminService
    {
        Task<PagedResult<LivestreamAdminListItemDTO>> GetListAsync(
            int? courseId, string? q, DateTime? from, DateTime? to, string? sort, int page, int pageSize);

        Task<Result<LivestreamAdminDetailDTO>> GetDetailAsync(int livestreamId);
        Task<Result<int>> CreateAsync(LivestreamCreateDTO dto);          // return new Id
        Task<Result<bool>> UpdateAsync(int livestreamId, LivestreamUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int livestreamId);

        Task<PagedResult<LivestreamRegistrationListItemDTO>> GetRegistrationsAsync(
            int livestreamId, string? paymentStatus, int page, int pageSize);

        Task<Result<bool>> UpdateRegistrationPaymentStatusAsync(int registrationId, UpdateRegistrationPaymentStatusDTO dto);
    }
}
