using Application.DTOs.Lessons;
using Application.Services.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class LessonStudentService : ILessonStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IProgressService _progressService;

        public LessonStudentService(IUnitOfWork uow, IProgressService progressService)
        {
            _uow = uow;
            _progressService = progressService;
        }

        public async Task<Result<IEnumerable<LessonDetailDTO>>> GetMyLessonsAsync(int accountId, int courseId)
        {
            var enrolled = await EnsureEnrolled(accountId, courseId);
            if (!enrolled) return Result<IEnumerable<LessonDetailDTO>>.Fail("Enrollment required.", 403);

            var enrollment = await _uow.EnrollmentRepository.GetAllQueryable()
                .FirstOrDefaultAsync(e => e.AccountId == accountId && e.CourseId == courseId && !e.IsDeleted && e.Status != EnrollmentStatus.Canceled);
            if (enrollment == null) return Result<IEnumerable<LessonDetailDTO>>.Fail("Enrollment required.", 403);

            var lc = _uow.LessonCompletionRepository.GetAllQueryable()
                .Where(x => x.EnrollmentId == enrollment.Id);

            var items = await _uow.LessonRepository.GetAllQueryable()
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.OrderIndex)
                .Select(l => new LessonDetailDTO
                {
                    LessonId = l.Id,
                    CourseId = l.CourseId,
                    Title = l.Title,
                    Description = l.Description,
                    VideoUrl = l.VideoUrl,
                    AttachmentUrl = l.AttachmentUrl,
                    OrderIndex = l.OrderIndex,
                    IsFreePreview = l.IsFreePreview,
                    Completed = lc.Any(c => c.LessonId == l.Id)
                })
                .ToListAsync();

            return Result<IEnumerable<LessonDetailDTO>>.Success(items);
        }

        public async Task<Result<LessonDetailDTO>> GetMyLessonDetailAsync(int accountId, int courseId, int lessonId)
        {
            var enrollment = await _uow.EnrollmentRepository.GetAllQueryable()
                .FirstOrDefaultAsync(e => e.AccountId == accountId && e.CourseId == courseId && !e.IsDeleted && e.Status != EnrollmentStatus.Canceled);
            if (enrollment == null) return Result<LessonDetailDTO>.Fail("Enrollment required.", 403);

            var l = await _uow.LessonRepository.GetAllQueryable()
                .FirstOrDefaultAsync(x => x.Id == lessonId && x.CourseId == courseId);
            if (l == null) return Result<LessonDetailDTO>.Fail("Lesson not found.", 404);

            var completed = await _uow.LessonCompletionRepository.GetAllQueryable()
                .AnyAsync(c => c.LessonId == lessonId && c.EnrollmentId == enrollment.Id);

            var dto = new LessonDetailDTO
            {
                LessonId = l.Id,
                CourseId = l.CourseId,
                Title = l.Title,
                Description = l.Description,
                VideoUrl = l.VideoUrl,
                AttachmentUrl = l.AttachmentUrl,
                OrderIndex = l.OrderIndex,
                IsFreePreview = l.IsFreePreview,
                Completed = completed
            };
            return Result<LessonDetailDTO>.Success(dto);
        }

        public async Task<Result<object>> CompleteLessonAsync(int accountId, int courseId, int lessonId)
        {
            // ensure enrollment
            var enrollment = await _uow.EnrollmentRepository.GetAllQueryable()
                .FirstOrDefaultAsync(e => e.AccountId == accountId && e.CourseId == courseId && !e.IsDeleted && e.Status != EnrollmentStatus.Canceled);
            if (enrollment == null) return Result<object>.Fail("Enrollment required.", 403);

            var lesson = await _uow.LessonRepository.GetAllQueryable().FirstOrDefaultAsync(l => l.Id == lessonId && l.CourseId == courseId);
            if (lesson == null) return Result<object>.Fail("Lesson not found.", 404);

            // add completion if not exists
            var exists = await _uow.LessonCompletionRepository.GetAllQueryable()
                .AnyAsync(lc => lc.EnrollmentId == enrollment.Id && lc.LessonId == lessonId);
            if (!exists)
            {
                await _uow.LessonCompletionRepository.AddAsync(new LessonCompletion
                {
                    EnrollmentId = enrollment.Id,
                    LessonId = lessonId,
                    CompletedAt = DateTime.UtcNow
                });
                await _uow.SaveChangesAsync();
            }

            // recalc percent = completed / total lessons * 100
            var totalLessons = await _uow.LessonRepository.GetAllQueryable()
                .CountAsync(l => l.CourseId == courseId);
            var completed = await _uow.LessonCompletionRepository.GetAllQueryable()
                .CountAsync(lc => lc.EnrollmentId == enrollment.Id);

            byte percent = 0;
            if (totalLessons > 0)
            {
                percent = (byte)Math.Min(100, (int)Math.Round((completed * 100.0) / totalLessons));
            }

            var update = await _progressService.UpdateMyProgressAsync(accountId, courseId, new DTOs.Progress.UpdateProgressRequest { CompletedPercent = percent });
            if (!update.Succeeded)
                return Result<object>.Fail(update.Message ?? "Failed to update progress.", update.StatusCode).AddError("progress", update.Message ?? "error");

            return Result<object>.Success(new { totalLessons, completed, percent }, "Lesson completed.", 200);
        }

        private async Task<bool> EnsureEnrolled(int accountId, int courseId)
        {
            return await _uow.EnrollmentRepository.GetAllQueryable()
                .AnyAsync(e => e.AccountId == accountId && e.CourseId == courseId && !e.IsDeleted && e.Status != EnrollmentStatus.Canceled);
        }
    }
}
