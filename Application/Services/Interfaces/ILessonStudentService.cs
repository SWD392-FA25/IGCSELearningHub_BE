using Application.DTOs.Courses;
using Application.DTOs.Lessons;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface ILessonStudentService
    {
        Task<Result<CourseDetailDTO>> GetMyCourseDetailAsync(int accountId, int courseId);
        Task<Result<IEnumerable<LessonDetailDTO>>> GetMyLessonsAsync(int accountId, int courseId);
        Task<Result<LessonDetailDTO>> GetMyLessonDetailAsync(int accountId, int courseId, int lessonId);
        Task<Result<object>> CompleteLessonAsync(int accountId, int courseId, int lessonId);
    }
}
