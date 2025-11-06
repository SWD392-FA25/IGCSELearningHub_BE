using System.Linq;
using Application.DTOs.Curricula;
using Application.Services.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CurriculumService : ICurriculumService
    {
        private readonly IUnitOfWork _uow;
        public CurriculumService(IUnitOfWork uow) => _uow = uow;

        public async Task<Result<IEnumerable<CurriculumDTO>>> GetByCourseAsync(int courseId)
        {
            var exists = await _uow.CourseRepository.GetAllQueryable()
                .AnyAsync(c => c.Id == courseId && !c.IsDeleted);
            if (!exists) return Result<IEnumerable<CurriculumDTO>>.Fail("Course not found.", 404);

            var data = await _uow.CurriculumRepository.GetAllQueryable()
                .Where(cu => cu.CourseId == courseId)
                .OrderBy(cu => cu.OrderIndex)
                .Select(cu => new CurriculumDTO
                {
                    CurriculumId = cu.Id,
                    CourseId = cu.CourseId,
                    Title = cu.Title,
                    Description = cu.Description,
                    OrderIndex = cu.OrderIndex
                })
                .ToListAsync();

            return Result<IEnumerable<CurriculumDTO>>.Success(data);
        }

        public async Task<Result<int>> CreateAsync(int courseId, CurriculumCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<int>.Fail("Title is required.", 400);

            var courseExists = await _uow.CourseRepository.GetAllQueryable()
                .AnyAsync(c => c.Id == courseId && !c.IsDeleted);
            if (!courseExists) return Result<int>.Fail("Course not found.", 404);

            var existingOrder = await _uow.CurriculumRepository.GetAllQueryable()
                .Where(cu => cu.CourseId == courseId)
                .Select(cu => (int?)cu.OrderIndex)
                .MaxAsync() ?? 0;

            var desiredOrder = dto.OrderIndex.GetValueOrDefault(existingOrder + 1);
            if (desiredOrder <= 0) desiredOrder = existingOrder + 1;

            var curriculum = new Curriculum
            {
                CourseId = courseId,
                Title = dto.Title.Trim(),
                Description = dto.Description,
                OrderIndex = desiredOrder
            };

            await _uow.CurriculumRepository.AddAsync(curriculum);
            await _uow.SaveChangesAsync();

            return Result<int>.Success(curriculum.Id, "Curriculum created.", 201);
        }

        public async Task<Result<CurriculumDTO>> GetByIdAsync(int curriculumId)
        {
            var curriculum = await _uow.CurriculumRepository.GetByIdAsync(curriculumId);
            if (curriculum == null)
                return Result<CurriculumDTO>.Fail("Curriculum not found.", 404);

            var dto = new CurriculumDTO
            {
                CurriculumId = curriculum.Id,
                CourseId = curriculum.CourseId,
                Title = curriculum.Title,
                Description = curriculum.Description,
                OrderIndex = curriculum.OrderIndex
            };

            return Result<CurriculumDTO>.Success(dto);
        }

        public async Task<Result<bool>> UpdateAsync(int curriculumId, CurriculumUpdateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<bool>.Fail("Title is required.", 400);

            var curriculum = await _uow.CurriculumRepository.GetByIdAsync(curriculumId);
            if (curriculum == null)
                return Result<bool>.Fail("Curriculum not found.", 404);

            curriculum.Title = dto.Title.Trim();
            curriculum.Description = dto.Description;
            if (dto.OrderIndex.HasValue && dto.OrderIndex.Value > 0)
            {
                curriculum.OrderIndex = dto.OrderIndex.Value;
            }

            _uow.CurriculumRepository.Update(curriculum);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Curriculum updated.", 200);
        }

        public async Task<Result<bool>> DeleteAsync(int curriculumId)
        {
            var curriculum = await _uow.CurriculumRepository.GetByIdAsync(curriculumId);
            if (curriculum == null)
                return Result<bool>.Fail("Curriculum not found.", 404);

            _uow.CurriculumRepository.SoftDelete(curriculum);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Curriculum deleted.", 200);
        }
    }
}
