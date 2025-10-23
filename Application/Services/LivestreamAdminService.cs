using Application.Services.Interfaces;
using Application.ViewModels.Livestreams;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class LivestreamAdminService : ILivestreamAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<LivestreamAdminService> _logger;

        public LivestreamAdminService(IUnitOfWork uow, ILogger<LivestreamAdminService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<PagedResult<LivestreamAdminListItemDTO>> GetListAsync(
            int? courseId, string? q, DateTime? from, DateTime? to, string? sort, int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is <= 0 or > 100 ? 20 : pageSize;

            var query = _uow.LivestreamRepository.GetAllQueryable($"{nameof(Livestream.LivestreamRegistrations)}");

            if (courseId.HasValue) query = query.Where(x => x.CourseId == courseId.Value);

            if (!string.IsNullOrWhiteSpace(q))
            {
                var key = q.Trim().ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(key));
            }

            if (from.HasValue) query = query.Where(x => x.Schedule >= from.Value);
            if (to.HasValue) query = query.Where(x => x.Schedule <= to.Value);

            // sort: schedule_desc|schedule_asc|price_asc|price_desc|title_asc|title_desc|createdAt_asc
            query = (sort ?? "").ToLower() switch
            {
                "schedule_asc" => query.OrderBy(x => x.Schedule),
                "price_asc" => query.OrderBy(x => x.Price),
                "price_desc" => query.OrderByDescending(x => x.Price),
                "title_asc" => query.OrderBy(x => x.Title),
                "title_desc" => query.OrderByDescending(x => x.Title),
                "createdat_asc" => query.OrderBy(x => x.CreatedAt),
                _ => query.OrderByDescending(x => x.Schedule) // mặc định: sắp tới trước
            };

            var total = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new LivestreamAdminListItemDTO
                {
                    Id = x.Id,
                    CourseId = x.CourseId,
                    TeacherId = x.TeacherId,
                    Title = x.Title,
                    Schedule = x.Schedule,
                    Price = x.Price,
                    RegistrationCount = x.LivestreamRegistrations.Count(r => !r.IsDeleted)
                })
                .ToListAsync();

            return PagedResult<LivestreamAdminListItemDTO>.Success(data, total, page, pageSize);
        }

        public async Task<Result<LivestreamAdminDetailDTO>> GetDetailAsync(int livestreamId)
        {
            var l = await _uow.LivestreamRepository.GetAllQueryable($"{nameof(Livestream.LivestreamRegistrations)}")
                .FirstOrDefaultAsync(x => x.Id == livestreamId);

            if (l == null) return Result<LivestreamAdminDetailDTO>.Fail("Livestream not found.", 404);

            var dto = new LivestreamAdminDetailDTO
            {
                Id = l.Id,
                CourseId = l.CourseId,
                TeacherId = l.TeacherId,
                Title = l.Title,
                Schedule = l.Schedule,
                Price = l.Price,
                RegistrationCount = l.LivestreamRegistrations.Count(r => !r.IsDeleted)
            };

            return Result<LivestreamAdminDetailDTO>.Success(dto);
        }

        public async Task<Result<int>> CreateAsync(LivestreamCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<int>.Fail("Title is required.", 400);
            if (dto.Price < 0)
                return Result<int>.Fail("Price must be >= 0.", 400);

            // validate Course & Teacher tồn tại
            var course = await _uow.CourseRepository.GetByIdAsync(dto.CourseId);
            if (course == null) return Result<int>.Fail("Course not found.", 404);

            var teacher = await _uow.AccountRepository.GetByIdAsync(dto.TeacherId);
            if (teacher == null) return Result<int>.Fail("Teacher not found.", 404);
            // (Optional) kiểm tra role Teacher ở đây nếu bạn đổi Status sang enum/role rõ ràng

            var l = new Livestream
            {
                CourseId = dto.CourseId,
                TeacherId = dto.TeacherId,
                Title = dto.Title.Trim(),
                Schedule = dto.Schedule,
                Price = dto.Price
            };

            await _uow.LivestreamRepository.AddAsync(l);
            await _uow.SaveChangesAsync();

            return Result<int>.Success(l.Id, "Created", 201);
        }

        public async Task<Result<bool>> UpdateAsync(int livestreamId, LivestreamUpdateDTO dto)
        {
            var l = await _uow.LivestreamRepository.GetByIdAsync(livestreamId);
            if (l == null) return Result<bool>.Fail("Livestream not found.", 404);

            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<bool>.Fail("Title is required.", 400);
            if (dto.Price < 0)
                return Result<bool>.Fail("Price must be >= 0.", 400);

            l.Title = dto.Title.Trim();
            l.Schedule = dto.Schedule;
            l.Price = dto.Price;

            if (dto.TeacherId.HasValue)
            {
                var teacher = await _uow.AccountRepository.GetByIdAsync(dto.TeacherId.Value);
                if (teacher == null) return Result<bool>.Fail("Teacher not found.", 404);
                l.TeacherId = dto.TeacherId.Value;
            }

            _uow.LivestreamRepository.Update(l);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Updated", 200);
        }

        public async Task<Result<bool>> DeleteAsync(int livestreamId)
        {
            var l = await _uow.LivestreamRepository.GetByIdAsync(livestreamId);
            if (l == null) return Result<bool>.Fail("Livestream not found.", 404);

            _uow.LivestreamRepository.SoftDelete(l);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Deleted", 200);
        }

        public async Task<PagedResult<LivestreamRegistrationListItemDTO>> GetRegistrationsAsync(
            int livestreamId, string? paymentStatus, int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is <= 0 or > 100 ? 20 : pageSize;

            // ensure livestream exists
            var exists = await _uow.LivestreamRepository.GetAllQueryable().AnyAsync(x => x.Id == livestreamId);
            if (!exists)
                return PagedResult<LivestreamRegistrationListItemDTO>.Success(new List<LivestreamRegistrationListItemDTO>(), 0, page, pageSize)
                    .AddDetail("warning", "Livestream not found") as PagedResult<LivestreamRegistrationListItemDTO>;

            var query = _uow.LivestreamRegistrationRepository
                .GetAllQueryable($"{nameof(LivestreamRegistration.Account)}")
                .Where(r => r.LivestreamId == livestreamId);

            if (!string.IsNullOrWhiteSpace(paymentStatus))
            {
                var ps = paymentStatus.Trim().ToLower();
                query = query.Where(r => r.PaymentStatus.ToLower() == ps);
            }

            query = query.OrderByDescending(r => r.RegisteredAt);

            var total = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new LivestreamRegistrationListItemDTO
                {
                    RegistrationId = r.Id,
                    AccountId = r.AccountId,
                    AccountUserName = r.Account.UserName,
                    PaymentStatus = r.PaymentStatus,
                    RegisteredAt = r.RegisteredAt
                })
                .ToListAsync();

            return PagedResult<LivestreamRegistrationListItemDTO>.Success(data, total, page, pageSize);
        }

        public async Task<Result<bool>> UpdateRegistrationPaymentStatusAsync(int registrationId, UpdateRegistrationPaymentStatusDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PaymentStatus))
                return Result<bool>.Fail("PaymentStatus is required.", 400);

            var reg = await _uow.LivestreamRegistrationRepository.GetByIdAsync(registrationId);
            if (reg == null) return Result<bool>.Fail("Registration not found.", 404);

            reg.PaymentStatus = dto.PaymentStatus.Trim();
            _uow.LivestreamRegistrationRepository.Update(reg);
            await _uow.SaveChangesAsync();

            return Result<bool>.Success(true, "Payment status updated", 200);
        }
    }
}
