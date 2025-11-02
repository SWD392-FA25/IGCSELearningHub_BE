using Application.DTOs.Livestreams;
using Application.Services.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Extensions;

namespace Application.Services
{
    public class LivestreamPublicService : ILivestreamPublicService
    {
        private readonly IUnitOfWork _uow;

        public LivestreamPublicService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<LivestreamPublicListItemDTO>> GetUpcomingListAsync(int? courseId, DateTime? from, DateTime? to, string? sort, int page, int pageSize)
        {
            var query = _uow.LivestreamRepository.GetAllQueryable();

            if (courseId.HasValue) query = query.Where(x => x.CourseId == courseId.Value);

            var now = DateTime.UtcNow;
            var start = from ?? now;
            query = query.Where(x => x.Schedule >= start);
            if (to.HasValue) query = query.Where(x => x.Schedule <= to.Value);

            query = (sort ?? "").ToLower() switch
            {
                "schedule_desc" => query.OrderByDescending(x => x.Schedule),
                "price_asc" => query.OrderBy(x => x.Price),
                "price_desc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Schedule)
            };

            return await query.ToPagedResultAsync(page, pageSize, x => new LivestreamPublicListItemDTO
            {
                Id = x.Id,
                CourseId = x.CourseId,
                Title = x.Title,
                Schedule = x.Schedule,
                Price = x.Price
            });
        }

        public async Task<Result<LivestreamPublicDetailDTO>> GetDetailAsync(int livestreamId)
        {
            var l = await _uow.LivestreamRepository.GetByIdAsync(livestreamId);
            if (l == null) return Result<LivestreamPublicDetailDTO>.Fail("Livestream not found.", 404);

            var dto = new LivestreamPublicDetailDTO
            {
                Id = l.Id,
                CourseId = l.CourseId,
                TeacherId = l.TeacherId,
                Title = l.Title,
                Schedule = l.Schedule,
                Price = l.Price
            };

            return Result<LivestreamPublicDetailDTO>.Success(dto);
        }
    }
}
