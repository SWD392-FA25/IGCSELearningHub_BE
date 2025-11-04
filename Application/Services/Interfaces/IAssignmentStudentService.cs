using Application.DTOs.Assignments;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface IAssignmentStudentService
    {
        Task<PagedResult<StudentAssignmentItemDTO>> GetListByCourseAsync(
            int accountId,
            int courseId,
            bool? submitted,
            int page,
            int pageSize,
            string? sort);
    }
}
