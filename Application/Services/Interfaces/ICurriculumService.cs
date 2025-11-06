using Application.DTOs.Curricula;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface ICurriculumService
    {
        Task<Result<IEnumerable<CurriculumDTO>>> GetByCourseAsync(int courseId);
        Task<Result<CurriculumDTO>> GetByIdAsync(int curriculumId);
        Task<Result<int>> CreateAsync(int courseId, CurriculumCreateDTO dto);
        Task<Result<bool>> UpdateAsync(int curriculumId, CurriculumUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int curriculumId);
    }
}
