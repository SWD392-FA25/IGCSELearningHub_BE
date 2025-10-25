using Application.DTOs.Submissions;
using Application.Services.Interfaces;
using Application.Extensions;
using Application.Utils.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentSubmissionService : IStudentSubmissionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<StudentSubmissionService> _logger;

        public StudentSubmissionService(IUnitOfWork uow, IDateTimeProvider clock, ILogger<StudentSubmissionService> logger)
        {
            _uow = uow;
            _clock = clock;
            _logger = logger;
        }

        public async Task<Result<SubmissionDTO>> SubmitAsync(int accountId, int assignmentId, CreateSubmissionRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.AttachmentUrl))
                return Result<SubmissionDTO>.Fail("Attachment URL is required.", 400);

            var assignment = await _uow.AssignmentRepository.GetAllQueryable($"{nameof(Assignment.Course)}")
                .FirstOrDefaultAsync(a => a.Id == assignmentId);
            if (assignment == null)
                return Result<SubmissionDTO>.Fail("Assignment not found.", 404);

            var enrolled = await _uow.EnrollmentRepository.GetAllQueryable()
                .AnyAsync(e => e.AccountId == accountId && e.CourseId == assignment.CourseId && !e.IsDeleted);
            if (!enrolled)
                return Result<SubmissionDTO>.Fail("You are not enrolled in this course.", 403);

            var sub = new Submission
            {
                AssignmentId = assignmentId,
                AccountId = accountId,
                SubmittedDate = _clock.UtcNow,
                AttachmentUrl = req.AttachmentUrl,
                TextAnswer = req.TextAnswer
            };

            await _uow.SubmissionRepository.AddAsync(sub);
            await _uow.SaveChangesAsync();

            var dto = new SubmissionDTO
            {
                SubmissionId = sub.Id,
                AssignmentId = sub.AssignmentId,
                AccountId = sub.AccountId,
                Score = sub.Score,
                SubmittedDate = sub.SubmittedDate,
                AttachmentUrl = sub.AttachmentUrl,
                TextAnswer = sub.TextAnswer
            };

            return Result<SubmissionDTO>.Success(dto, "Submission created successfully.", 201);
        }

        public async Task<PagedResult<SubmissionDTO>> GetMySubmissionsAsync(int accountId, int assignmentId, int page, int pageSize)
        {
            var q = _uow.SubmissionRepository.GetAllQueryable()
                .Where(s => s.AccountId == accountId && s.AssignmentId == assignmentId)
                .OrderByDescending(s => s.SubmittedDate);

            return await q.ToPagedResultAsync(page, pageSize, s => new SubmissionDTO
            {
                SubmissionId = s.Id,
                AssignmentId = s.AssignmentId,
                AccountId = s.AccountId,
                Score = s.Score,
                SubmittedDate = s.SubmittedDate,
                AttachmentUrl = s.AttachmentUrl,
                TextAnswer = s.TextAnswer
            });
        }
    }
}
