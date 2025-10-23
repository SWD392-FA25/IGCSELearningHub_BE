using Application.ViewModels.Assignments;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAssignmentAdminService
    {
        Task<PagedResult<AssignmentAdminListItemDTO>> GetListAsync(int? courseId, string? q, int page, int pageSize, string? sort);
        Task<Result<AssignmentAdminDetailDTO>> GetDetailAsync(int assignmentId);
        Task<Result<int>> CreateAsync(AssignmentCreateDTO dto); // return new AssignmentId
        Task<Result<bool>> UpdateAsync(int assignmentId, AssignmentUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int assignmentId);

        // Submissions (giáo viên xem/chấm)
        Task<PagedResult<SubmissionListItemDTO>> GetSubmissionsAsync(int assignmentId, int page, int pageSize);
        Task<Result<SubmissionDetailDTO>> GetSubmissionDetailAsync(int submissionId);
        Task<Result<bool>> GradeSubmissionAsync(int submissionId, GradeSubmissionDto dto);
    }
}
