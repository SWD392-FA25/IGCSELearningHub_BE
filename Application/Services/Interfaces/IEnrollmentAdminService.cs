using Application.ViewModels.Enrollments;
using Application.Wrappers;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IEnrollmentAdminService
    {
        Task<PagedResult<EnrollmentAdminListItemDTO>> GetListAsync(
            int? accountId, int? courseId, EnrollmentStatus? status, DateTime? from, DateTime? to,
            int page, int pageSize, string? sort);

        Task<Result<EnrollmentAdminDetailDTO>> GetDetailAsync(int enrollmentId);
        Task<Result<int>> CreateAsync(EnrollmentCreateDTO dto);          // return new EnrollmentId
        Task<Result<bool>> UpdateStatusAsync(int enrollmentId, EnrollmentUpdateStatusDTO dto);
        Task<Result<bool>> DeleteAsync(int enrollmentId);                // soft delete

        // Tạo enrollment từ đơn hàng đã thanh toán (idempotent)
        Task<Result<int>> CreateFromOrderAsync(int orderId);             // return số enrollment được tạo
    }
}
