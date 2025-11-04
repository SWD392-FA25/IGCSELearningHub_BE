using Application.DTOs.Lessons;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface ILessonAdminService
    {
        Task<Result<int>> CreateAsync(int courseId, LessonCreateDTO dto);
        Task<Result<bool>> UpdateAsync(int lessonId, LessonUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int lessonId);
        Task<Result<bool>> UpdateOrderAsync(int lessonId, LessonOrderUpdateDTO dto);
    }
}

