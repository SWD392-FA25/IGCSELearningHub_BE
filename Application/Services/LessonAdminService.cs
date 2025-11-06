using Application.DTOs.Lessons;
using Application.Services.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class LessonAdminService : ILessonAdminService
    {
        private readonly IUnitOfWork _uow;

        public LessonAdminService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<int>> CreateAsync(int courseId, LessonCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<int>.Fail("Title is required.", 400);

            var course = await _uow.CourseRepository.GetByIdAsync(courseId);
            if (course == null) return Result<int>.Fail("Course not found.", 404);

            var unit = await _uow.UnitRepository.GetByIdAsync(dto.UnitId);
            if (unit == null || unit.CourseId != courseId)
                return Result<int>.Fail("Unit not found in this course.", 404);

            // order within unit
            var q = _uow.LessonRepository
                .GetAllQueryable()
                .Where(l => l.UnitId == dto.UnitId);

            int nextOrder;
            var any = await q.AnyAsync();
            if (any)
            {
                var currentMax = await q.MaxAsync(l => l.OrderIndex);
                nextOrder = currentMax + 1;
            }
            else
            {
                nextOrder = 1;
            }

            var lesson = new Lesson
            {
                CourseId = courseId,
                UnitId = dto.UnitId,
                Title = dto.Title.Trim(),
                Description = dto.Description,
                VideoUrl = dto.VideoUrl,
                AttachmentUrl = dto.AttachmentUrl,
                IsFreePreview = dto.IsFreePreview ?? false,
                OrderIndex = nextOrder
            };

            await _uow.LessonRepository.AddAsync(lesson);
            await _uow.SaveChangesAsync();

            return Result<int>.Success(lesson.Id, "Created", 201);
        }

        public async Task<Result<bool>> UpdateAsync(int lessonId, LessonUpdateDTO dto)
        {
            var lesson = await _uow.LessonRepository.GetByIdAsync(lessonId);
            if (lesson == null) return Result<bool>.Fail("Lesson not found.", 404);

            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<bool>.Fail("Title is required.", 400);

            if (dto.UnitId.HasValue && dto.UnitId.Value != lesson.UnitId)
            {
                var unit = await _uow.UnitRepository.GetByIdAsync(dto.UnitId.Value);
                if (unit == null || unit.CourseId != lesson.CourseId)
                    return Result<bool>.Fail("Invalid unit for this course.", 400);

                lesson.UnitId = dto.UnitId.Value;
                var maxOrder = await _uow.LessonRepository.GetAllQueryable()
                    .Where(l => l.UnitId == lesson.UnitId && l.Id != lesson.Id)
                    .Select(l => (int?)l.OrderIndex)
                    .MaxAsync() ?? 0;
                lesson.OrderIndex = maxOrder + 1;
            }

            lesson.Title = dto.Title.Trim();
            lesson.Description = dto.Description;
            lesson.VideoUrl = dto.VideoUrl;
            lesson.AttachmentUrl = dto.AttachmentUrl;
            if (dto.IsFreePreview.HasValue) lesson.IsFreePreview = dto.IsFreePreview.Value;

            _uow.LessonRepository.Update(lesson);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Updated", 200);
        }

        public async Task<Result<bool>> DeleteAsync(int lessonId)
        {
            var lesson = await _uow.LessonRepository.GetByIdAsync(lessonId);
            if (lesson == null) return Result<bool>.Fail("Lesson not found.", 404);

            _uow.LessonRepository.SoftDelete(lesson);
            await _uow.SaveChangesAsync();
            return Result<bool>.Success(true, "Deleted", 200);
        }

        public async Task<Result<bool>> UpdateOrderAsync(int lessonId, LessonOrderUpdateDTO dto)
        {
            if (dto.OrderIndex < 0)
                return Result<bool>.Fail("OrderIndex must be >= 0.", 400);

            var lesson = await _uow.LessonRepository.GetByIdAsync(lessonId);
            if (lesson == null) return Result<bool>.Fail("Lesson not found.", 404);

            lesson.OrderIndex = dto.OrderIndex;
            _uow.LessonRepository.Update(lesson);
            await _uow.SaveChangesAsync();
            return Result<bool>.Success(true, "OrderIndex updated", 200);
        }
    }
}
