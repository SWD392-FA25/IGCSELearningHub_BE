using Application.DTOs.Submissions;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAdminSubmissionService
    {
        Task<Result<bool>> ScoreAsync(int submissionId, decimal score);
        Task<PagedResult<SubmissionAdminListItemDto>> GetAssignmentSubmissionsAsync(int assignmentId, int? studentId, int page, int pageSize);
    }
}
