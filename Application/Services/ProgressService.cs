using Application.Services.Interfaces;
using Application.Utils.Interfaces;
using Application.Wrappers;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.DTOs.Progress;

namespace Application.Services
{
    public class ProgressService : IProgressService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<ProgressService> _logger;

        public ProgressService(IUnitOfWork uow, IDateTimeProvider clock, ILogger<ProgressService> logger)
        {
            _uow = uow;
            _clock = clock;
            _logger = logger;
        }

        // ===== Student =====
        public async Task<Result<StudentProgressDTO>> GetMyProgressAsync(int accountId, int courseId)
        {
            var enrollment = await _uow.EnrollmentRepository.GetAllQueryable($"{nameof(Enrollment.Progresses)},{nameof(Enrollment.Course)}")
                .FirstOrDefaultAsync(e => e.AccountId == accountId && e.CourseId == courseId && !e.IsDeleted);

            if (enrollment == null)
                return Result<StudentProgressDTO>.Fail("Enrollment not found.", 404);

            var progress = enrollment.Progresses.FirstOrDefault();
            if (progress == null)
            {
                return Result<StudentProgressDTO>.Success(new StudentProgressDTO
                {
                    CourseId = courseId,
                    CourseTitle = enrollment.Course.Title,
                    CompletedPercent = 0,
                    LastAccessDate = null
                });
            }

            return Result<StudentProgressDTO>.Success(new StudentProgressDTO
            {
                CourseId = courseId,
                CourseTitle = enrollment.Course.Title,
                CompletedPercent = progress.CompletedPercent,
                LastAccessDate = progress.LastAccessDate
            });
        }

        public async Task<Result<StudentProgressDTO>> UpdateMyProgressAsync(int accountId, int courseId, UpdateProgressRequest req)
        {
            if (req.CompletedPercent > 100)
                return Result<StudentProgressDTO>.Fail("CompletedPercent cannot exceed 100.", 400);

            var enrollment = await _uow.EnrollmentRepository.GetAllQueryable($"{nameof(Enrollment.Progresses)},{nameof(Enrollment.Course)}")
                .FirstOrDefaultAsync(e => e.AccountId == accountId && e.CourseId == courseId && !e.IsDeleted);

            if (enrollment == null)
                return Result<StudentProgressDTO>.Fail("Enrollment not found.", 404);

            var progress = enrollment.Progresses.FirstOrDefault();
            if (progress == null)
            {
                progress = new Domain.Entities.Progress
                {
                    EnrollmentId = enrollment.Id,
                    CompletedPercent = req.CompletedPercent,
                    LastAccessDate = _clock.UtcNow
                };
                await _uow.ProgressRepository.AddAsync(progress);
            }
            else
            {
                progress.CompletedPercent = req.CompletedPercent;
                progress.LastAccessDate = _clock.UtcNow;
                _uow.ProgressRepository.Update(progress);
            }

            await _uow.SaveChangesAsync();

            return Result<StudentProgressDTO>.Success(new StudentProgressDTO
            {
                CourseId = courseId,
                CourseTitle = enrollment.Course.Title,
                CompletedPercent = progress.CompletedPercent,
                LastAccessDate = progress.LastAccessDate
            }, "Progress updated.", 200);
        }

        // ===== Admin =====
        public async Task<PagedResult<AdminProgressDTO>> GetCourseProgressAsync(int courseId, int page, int pageSize)
        {
            page = Math.Max(page, 1);
            pageSize = pageSize is > 0 and <= 100 ? pageSize : 20;

            var query = _uow.ProgressRepository.GetAllQueryable($"{nameof(Progress.Enrollment)},{nameof(Progress.Enrollment.Account)}")
                .Where(p => p.Enrollment.CourseId == courseId && !p.IsDeleted);

            query = query.OrderByDescending(p => p.ModifiedAt);
            return await query.ToPagedResultAsync(page, pageSize, p => new AdminProgressDTO
            {
                AccountId = p.Enrollment.AccountId,
                StudentName = p.Enrollment.Account.FullName ?? p.Enrollment.Account.UserName,
                EnrollmentId = p.EnrollmentId,
                CompletedPercent = p.CompletedPercent,
                LastAccessDate = p.LastAccessDate
            });
        }
    }
}
