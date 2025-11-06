using Application.DTOs.Lessons;
using Application.Services.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class LessonPublicService : ILessonPublicService
    {
        private readonly IUnitOfWork _uow;
        public LessonPublicService(IUnitOfWork uow) => _uow = uow;

        public async Task<Result<IEnumerable<LessonListItemDTO>>> GetLessonsAsync(int courseId, int? accountId)
        {
            var q = _uow.LessonRepository.GetAllQueryable($"{nameof(Lesson.Curriculum)}")
                .Where(l => l.CourseId == courseId);

            var isEnrolled = await IsEnrolledAsync(accountId, courseId);
            if (!isEnrolled)
            {
                q = q.Where(l => l.IsFreePreview);
            }

            var items = await q
                .OrderBy(l => l.Curriculum.OrderIndex)
                .ThenBy(l => l.OrderIndex)
                .Select(l => new LessonListItemDTO
                {
                    LessonId = l.Id,
                    CourseId = l.CourseId,
                    CurriculumId = l.CurriculumId,
                    Title = l.Title,
                    Description = l.Description,
                    OrderIndex = l.OrderIndex,
                    IsFreePreview = l.IsFreePreview,
                    Completed = false
                })
                .ToListAsync();

            return Result<IEnumerable<LessonListItemDTO>>.Success(items);
        }

        public async Task<Result<LessonDetailDTO>> GetLessonDetailAsync(int courseId, int lessonId, int? accountId)
        {
            var lesson = await _uow.LessonRepository
                .GetAllQueryable()
                .FirstOrDefaultAsync(l => l.Id == lessonId && l.CourseId == courseId);

            if (lesson == null) return Result<LessonDetailDTO>.Fail("Lesson not found.", 404);

            if (!lesson.IsFreePreview)
            {
                var isEnrolled = await IsEnrolledAsync(accountId, courseId);
                if (!isEnrolled)
                    return Result<LessonDetailDTO>.Fail("Enrollment required to view this lesson.", 403);
            }

            var dto = new LessonDetailDTO
            {
                LessonId = lesson.Id,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Description = lesson.Description,
                VideoUrl = lesson.VideoUrl,
                AttachmentUrl = lesson.AttachmentUrl,
                OrderIndex = lesson.OrderIndex,
                IsFreePreview = lesson.IsFreePreview,
                CurriculumId = lesson.CurriculumId,
                Completed = false
            };

            return Result<LessonDetailDTO>.Success(dto);
        }

        private async Task<bool> IsEnrolledAsync(int? accountId, int courseId)
        {
            if (!accountId.HasValue) return false;
            var enrolled = await _uow.EnrollmentRepository.GetAllQueryable()
                .AnyAsync(e => e.AccountId == accountId.Value
                               && e.CourseId == courseId
                               && !e.IsDeleted
                               && e.Status != EnrollmentStatus.Canceled);
            return enrolled;
        }
    }
}
