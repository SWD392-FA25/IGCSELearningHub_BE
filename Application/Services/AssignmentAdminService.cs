using Application.Services.Interfaces;
using Application.DTOs.Assignments;
using Application.Wrappers;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AssignmentAdminService : IAssignmentAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<AssignmentAdminService> _logger;

        public AssignmentAdminService(IUnitOfWork uow, ILogger<AssignmentAdminService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<PagedResult<AssignmentAdminListItemDTO>> GetListAsync(int? courseId, string? q, int page, int pageSize, string? sort)
        {
            var query = _uow.AssignmentRepository.GetAllQueryable($"{nameof(Assignment.Submissions)}");

            if (courseId.HasValue) query = query.Where(a => a.CourseId == courseId.Value);

            if (!string.IsNullOrWhiteSpace(q))
            {
                var key = q.Trim().ToLower();
                query = query.Where(a => a.Title.ToLower().Contains(key) ||
                                        (a.Description != null && a.Description.ToLower().Contains(key)));
            }

            query = sort?.ToLower() switch
            {
                "title_asc" => query.OrderBy(a => a.Title),
                "title_desc" => query.OrderByDescending(a => a.Title),
                "createdat_asc" => query.OrderBy(a => a.CreatedAt),
                _ => query.OrderByDescending(a => a.CreatedAt)
            };

            return await query.ToPagedResultAsync(page, pageSize, a => new AssignmentAdminListItemDTO
            {
                Id = a.Id,
                CourseId = a.CourseId,
                Title = a.Title,
                CreatedAt = a.CreatedAt,
                SubmissionCount = a.Submissions.Count(x => !x.IsDeleted)
            });
        }

        public async Task<Result<AssignmentAdminDetailDTO>> GetDetailAsync(int assignmentId)
        {
            var a = await _uow.AssignmentRepository.GetAllQueryable($"{nameof(Assignment.Submissions)}")
                .FirstOrDefaultAsync(x => x.Id == assignmentId);

            if (a == null) return Result<AssignmentAdminDetailDTO>.Fail("Assignment not found.", 404);

            var dto = new AssignmentAdminDetailDTO
            {
                Id = a.Id,
                CourseId = a.CourseId,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                SubmissionCount = a.Submissions.Count(x => !x.IsDeleted)
            };

            return Result<AssignmentAdminDetailDTO>.Success(dto);
        }

        public async Task<Result<int>> CreateAsync(AssignmentCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<int>.Fail("Title is required.", 400);

            // đảm bảo Course tồn tại
            var course = await _uow.CourseRepository.GetByIdAsync(dto.CourseId);
            if (course == null) return Result<int>.Fail("Course not found.", 404);

            var assignment = new Assignment
            {
                CourseId = dto.CourseId,
                Title = dto.Title.Trim(),
                Description = dto.Description
            };

            await _uow.AssignmentRepository.AddAsync(assignment);
            await _uow.SaveChangesAsync();

            return Result<int>.Success(assignment.Id, "Created", 201);
        }

        public async Task<Result<bool>> UpdateAsync(int assignmentId, AssignmentUpdateDTO dto)
        {
            var a = await _uow.AssignmentRepository.GetByIdAsync(assignmentId);
            if (a == null) return Result<bool>.Fail("Assignment not found.", 404);

            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<bool>.Fail("Title is required.", 400);

            a.Title = dto.Title.Trim();
            a.Description = dto.Description;

            _uow.AssignmentRepository.Update(a);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Updated", 200);
        }

        public async Task<Result<bool>> DeleteAsync(int assignmentId)
        {
            var a = await _uow.AssignmentRepository.GetByIdAsync(assignmentId);
            if (a == null) return Result<bool>.Fail("Assignment not found.", 404);

            _uow.AssignmentRepository.SoftDelete(a);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Deleted", 200);
        }

        // ===== Submissions =====

        public async Task<PagedResult<SubmissionListItemDTO>> GetSubmissionsAsync(int assignmentId, int page, int pageSize)
        {
            // ensure assignment exists
            var exists = await _uow.AssignmentRepository.GetAllQueryable().AnyAsync(x => x.Id == assignmentId);
            if (!exists) return PagedResult<SubmissionListItemDTO>.Success(new List<SubmissionListItemDTO>(), 0, page, pageSize)
                .AddDetail("warning", "Assignment not found") as PagedResult<SubmissionListItemDTO>;

            var query = _uow.SubmissionRepository.GetAllQueryable($"{nameof(Submission.Account)}")
                .Where(s => s.AssignmentId == assignmentId)
                .OrderByDescending(s => s.SubmittedDate);

            return await query.ToPagedResultAsync(page, pageSize, s => new SubmissionListItemDTO
            {
                SubmissionId = s.Id,
                AccountId = s.AccountId,
                AccountUserName = s.Account.UserName,
                Score = s.Score,
                SubmittedDate = s.SubmittedDate
            });
        }

        public async Task<Result<SubmissionDetailDTO>> GetSubmissionDetailAsync(int submissionId)
        {
            var s = await _uow.SubmissionRepository.GetAllQueryable($"{nameof(Submission.Account)}")
                .FirstOrDefaultAsync(x => x.Id == submissionId);

            if (s == null) return Result<SubmissionDetailDTO>.Fail("Submission not found.", 404);

            var dto = new SubmissionDetailDTO
            {
                SubmissionId = s.Id,
                AssignmentId = s.AssignmentId,
                AccountId = s.AccountId,
                AccountUserName = s.Account.UserName,
                Score = s.Score,
                SubmittedDate = s.SubmittedDate
            };

            return Result<SubmissionDetailDTO>.Success(dto);
        }

        public async Task<Result<bool>> GradeSubmissionAsync(int submissionId, GradeSubmissionDto dto)
        {
            var s = await _uow.SubmissionRepository.GetByIdAsync(submissionId);
            if (s == null) return Result<bool>.Fail("Submission not found.", 404);

            s.Score = dto.Score; // có thể kiểm tra min/max theo rubric nếu có
            _uow.SubmissionRepository.Update(s);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Graded", 200);
        }
    }
}
