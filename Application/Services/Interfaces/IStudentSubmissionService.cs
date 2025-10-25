using Application.DTOs.Submissions;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IStudentSubmissionService
    {
        Task<Result<SubmissionDto>> SubmitAsync(int accountId, int assignmentId, CreateSubmissionRequest req);
        Task<PagedResult<SubmissionDto>> GetMySubmissionsAsync(int accountId, int assignmentId, int page, int pageSize);
    }
}
