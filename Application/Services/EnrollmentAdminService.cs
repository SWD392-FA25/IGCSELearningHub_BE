using Application.Services.Interfaces;
using Application.Utils.Interfaces;
using Application.Extensions;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.DTOs.Enrollments;

namespace Application.Services
{
    public class EnrollmentAdminService : IEnrollmentAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<EnrollmentAdminService> _logger;

        public EnrollmentAdminService(IUnitOfWork uow, IDateTimeProvider clock, ILogger<EnrollmentAdminService> logger)
        {
            _uow = uow;
            _clock = clock;
            _logger = logger;
        }

        public async Task<PagedResult<EnrollmentAdminListItemDTO>> GetListAsync(
            int? accountId, int? courseId, EnrollmentStatus? status, DateTime? from, DateTime? to,
            int page, int pageSize, string? sort)
        {
            var query = _uow.EnrollmentRepository
                .GetAllQueryable($"{nameof(Enrollment.Account)},{nameof(Enrollment.Course)}");

            if (accountId.HasValue) query = query.Where(e => e.AccountId == accountId.Value);
            if (courseId.HasValue) query = query.Where(e => e.CourseId == courseId.Value);
            if (status.HasValue) query = query.Where(e => e.Status == status.Value);
            if (from.HasValue) query = query.Where(e => e.EnrollmentDate >= from.Value);
            if (to.HasValue) query = query.Where(e => e.EnrollmentDate <= to.Value);

            query = (sort ?? "").ToLower() switch
            {
                "createdat_asc" => query.OrderBy(e => e.EnrollmentDate),
                "title_asc" => query.OrderBy(e => e.Course.Title),
                "title_desc" => query.OrderByDescending(e => e.Course.Title),
                "user_asc" => query.OrderBy(e => e.Account.UserName),
                "user_desc" => query.OrderByDescending(e => e.Account.UserName),
                "status_asc" => query.OrderBy(e => e.Status),
                "status_desc" => query.OrderByDescending(e => e.Status),
                _ => query.OrderByDescending(e => e.EnrollmentDate)
            };

            return await query.ToPagedResultAsync(page, pageSize, e => new EnrollmentAdminListItemDTO
            {
                EnrollmentId = e.Id,
                AccountId = e.AccountId,
                AccountUserName = e.Account.UserName,
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status
            });
        }

        public async Task<Result<EnrollmentAdminDetailDTO>> GetDetailAsync(int enrollmentId)
        {
            var e = await _uow.EnrollmentRepository.GetAllQueryable(
                    $"{nameof(Enrollment.Account)},{nameof(Enrollment.Course)},{nameof(Enrollment.Progresses)}")
                .FirstOrDefaultAsync(x => x.Id == enrollmentId);

            if (e == null) return Result<EnrollmentAdminDetailDTO>.Fail("Enrollment not found.", 404);

            var dto = new EnrollmentAdminDetailDTO
            {
                EnrollmentId = e.Id,
                AccountId = e.AccountId,
                AccountUserName = e.Account.UserName,
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                CompletedPercent = e.Progresses.OrderByDescending(p => p.ModifiedAt).Select(p => (byte?)p.CompletedPercent).FirstOrDefault(),
                LastAccessDate = e.Progresses.OrderByDescending(p => p.LastAccessDate).Select(p => p.LastAccessDate).FirstOrDefault()
            };

            return Result<EnrollmentAdminDetailDTO>.Success(dto);
        }

        public async Task<Result<int>> CreateAsync(EnrollmentCreateDTO dto)
        {
            var acc = await _uow.AccountRepository.GetByIdAsync(dto.AccountId);
            if (acc == null) return Result<int>.Fail("Account not found.", 404);

            var course = await _uow.CourseRepository.GetByIdAsync(dto.CourseId);
            if (course == null) return Result<int>.Fail("Course not found.", 404);

            var exists = await _uow.EnrollmentRepository.GetAllQueryable()
                .AnyAsync(e => e.AccountId == dto.AccountId && e.CourseId == dto.CourseId && !e.IsDeleted && e.Status != EnrollmentStatus.Canceled);
            if (exists) return Result<int>.Fail("Already enrolled.", 400);

            var e = new Enrollment
            {
                AccountId = dto.AccountId,
                CourseId = dto.CourseId,
                EnrollmentDate = _clock.UtcNow,
                Status = dto.Status
            };

            await _uow.EnrollmentRepository.AddAsync(e);
            await _uow.SaveChangesAsync();
            return Result<int>.Success(e.Id, "Created", 201);
        }

        public async Task<Result<bool>> UpdateStatusAsync(int enrollmentId, EnrollmentUpdateStatusDTO dto)
        {
            var e = await _uow.EnrollmentRepository.GetByIdAsync(enrollmentId);
            if (e == null) return Result<bool>.Fail("Not found", 404);
            e.Status = dto.Status;
            _uow.EnrollmentRepository.Update(e);
            await _uow.SaveChangesAsync();
            return Result<bool>.Success(true, "Updated", 200);
        }

        public async Task<Result<bool>> DeleteAsync(int enrollmentId)
        {
            var e = await _uow.EnrollmentRepository.GetByIdAsync(enrollmentId);
            if (e == null) return Result<bool>.Fail("Not found", 404);
            _uow.EnrollmentRepository.SoftDelete(e);
            await _uow.SaveChangesAsync();
            return Result<bool>.Success(true, "Deleted", 200);
        }

        public async Task<Result<int>> CreateFromOrderAsync(int orderId)
        {
            var order = await _uow.OrderRepository.GetAllQueryable(
                    $"{nameof(Order.Payments)},{nameof(Order.OrderDetails)}")
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return Result<int>.Fail("OrderIndex not found.", 404);

            var hasPaid = order.Payments.Any(p => p.Status == PaymentStatus.Paid);
            if (!hasPaid) return Result<int>.Fail("No successful payment for this order.", 400);

            var courseIds = order.OrderDetails
                .Where(d => d.ItemType == ItemType.Course && !d.IsDeleted)
                .Select(d => d.ItemId)
                .ToList();

            var packageIds = order.OrderDetails
                .Where(d => d.ItemType == ItemType.CoursePackage && !d.IsDeleted)
                .Select(d => d.ItemId)
                .Distinct()
                .ToList();

            if (packageIds.Count > 0)
            {
                var packages = await _uow.CoursePackageRepository
                    .GetAllQueryable($"{nameof(CoursePackage.Courses)}")
                    .Where(p => packageIds.Contains(p.Id))
                    .ToListAsync();

                var expanded = packages
                    .SelectMany(p => p.Courses.Where(c => !c.IsDeleted).Select(c => c.Id));

                courseIds.AddRange(expanded);
            }

            var distinctCourseIds = courseIds.Distinct().ToList();
            if (distinctCourseIds.Count == 0)
                return Result<int>.Fail("OrderIndex has no course items.", 400);

            int created = 0;
            using var tx = await _uow.BeginTransactionAsync();
            try
            {
                foreach (var courseId in distinctCourseIds)
                {
                    var already = await _uow.EnrollmentRepository.GetAllQueryable()
                        .AnyAsync(e => e.AccountId == order.AccountId
                                       && e.CourseId == courseId
                                       && !e.IsDeleted
                                       && e.Status != EnrollmentStatus.Canceled);

                    if (already) continue;

                    await _uow.EnrollmentRepository.AddAsync(new Enrollment
                    {
                        AccountId = order.AccountId,
                        CourseId = courseId,
                        EnrollmentDate = _clock.UtcNow,
                        Status = EnrollmentStatus.Active
                    });
                    created++;
                }

                var livestreamIds = order.OrderDetails
                    .Where(d => d.ItemType == ItemType.Livestream && !d.IsDeleted)
                    .Select(d => d.ItemId)
                    .Distinct()
                    .ToList();

                foreach (var lsId in livestreamIds)
                {
                    var exists = await _uow.LivestreamRegistrationRepository.GetAllQueryable()
                        .AnyAsync(r => r.LivestreamId == lsId
                                       && r.AccountId == order.AccountId
                                       && !r.IsDeleted);
                    if (exists) continue;

                    await _uow.LivestreamRegistrationRepository.AddAsync(new LivestreamRegistration
                    {
                        LivestreamId = lsId,
                        AccountId = order.AccountId,
                        PaymentStatus = "Paid",
                        RegisteredAt = _clock.UtcNow
                    });
                }

                await _uow.SaveChangesAsync();
                await tx.CommitAsync();

                return Result<int>.Success(created, "Enrollments created from order", 201)
                    .AddDetail("orderId", orderId)
                    .AddDetail("coursesEnrolled", distinctCourseIds);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "CreateFromOrderAsync failed for OrderId {OrderId}", orderId);
                return Result<int>.Fail("Failed to create enrollments from order.", 500)
                    .AddError("exception", ex.Message);
            }
        }
    }
}
