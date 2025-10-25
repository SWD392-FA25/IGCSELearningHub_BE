using Application.DTOs.Submissions;
using Application.Services.Interfaces;
using Application.Wrappers;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminSubmissionService : IAdminSubmissionService
    {
        private readonly IUnitOfWork _uow;

        public AdminSubmissionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<bool>> ScoreAsync(int submissionId, decimal score)
        {
            if (score < 0 || score > 100)
                return Result<bool>.Fail("Score must be between 0 and 100.", 400);

            var sub = await _uow.SubmissionRepository.GetByIdAsync(submissionId);
            if (sub == null)
                return Result<bool>.Fail("Submission not found.", 404);

            sub.Score = score;
            _uow.SubmissionRepository.Update(sub);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Submission scored successfully.");
        }

        public async Task<PagedResult<SubmissionAdminListItemDto>> GetAssignmentSubmissionsAsync(int assignmentId, int? studentId, int page, int pageSize)
        {
            var q = _uow.SubmissionRepository.GetAllQueryable($"{nameof(Submission.Account)}")
                .Where(s => s.AssignmentId == assignmentId);

            if (studentId.HasValue)
                q = q.Where(s => s.AccountId == studentId.Value);

            q = q.OrderByDescending(s => s.SubmittedDate);
            return await q.ToPagedResultAsync(page, pageSize, s => new SubmissionAdminListItemDto
            {
                SubmissionId = s.Id,
                AssignmentId = s.AssignmentId,
                AccountId = s.AccountId,
                StudentName = s.Account.FullName ?? s.Account.UserName,
                SubmittedDate = s.SubmittedDate,
                Score = s.Score,
                AttachmentUrl = s.AttachmentUrl
            });
        }
    }
}
