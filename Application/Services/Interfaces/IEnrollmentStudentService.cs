using Application.DTOs.Enrollments;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IEnrollmentStudentService
    {
        Task<PagedResult<MyEnrollmentItemDTO>> GetMyEnrollmentsAsync(int accountId, int page, int pageSize);
        Task<Result<MyEnrollmentDetailDTO>> GetMyEnrollmentDetailAsync(int accountId, int enrollmentId);
    }
}
