using Application.DTOs.Units;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface IUnitService
    {
        Task<Result<IEnumerable<UnitDTO>>> GetByCourseAsync(int courseId);
        Task<Result<UnitDTO>> GetByIdAsync(int unitId);
        Task<Result<int>> CreateAsync(int courseId, UnitCreateDTO dto);
        Task<Result<bool>> UpdateAsync(int unitId, UnitUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int unitId);
    }
}
