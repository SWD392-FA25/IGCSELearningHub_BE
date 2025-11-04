using Application.DTOs.Lessons;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface ILessonPublicService
    {
        Task<Result<IEnumerable<LessonListItemDTO>>> GetLessonsAsync(int courseId, int? accountId);
        Task<Result<LessonDetailDTO>> GetLessonDetailAsync(int courseId, int lessonId, int? accountId);
    }
}

