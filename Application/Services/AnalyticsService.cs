using Application.DTOs.Analytics;
using Application.Services.Interfaces;
using Application.Wrappers;
using Application.Utils.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(IUnitOfWork uow, IDateTimeProvider clock, ILogger<AnalyticsService> logger)
        {
            _uow = uow;
            _clock = clock;
            _logger = logger;
        }

        // ========= Helpers =========

        private (DateTime from, DateTime to) NormalizeRange(DateRangeQuery q)
        {
            var to = q.To?.ToUniversalTime() ?? _clock.UtcNow;
            var from = q.From?.ToUniversalTime() ?? to.AddDays(-30);
            if (from > to) (from, to) = (to, from);
            return (from, to);
        }

        private static IQueryable<T> Between<T>(IQueryable<T> q, Func<T, DateTime> selector, DateTime from, DateTime to)
        {
            // Placeholder: keep server-side composed queries elsewhere
            return q;
        }

        private static IQueryable<TResult> GroupByPeriod<TSource, TResult>(
            IQueryable<TSource> source,
            Func<TSource, DateTime> dateSelector,
            GroupBy gb,
            Func<IQueryable<TSource>, IQueryable<TResult>> projector)
        {
            return projector(source);
        }

        // ========= KPIs =========

        public async Task<Result<KpiSummaryDTO>> GetKpisAsync(DateRangeQuery q)
        {
            var (from, to) = NormalizeRange(q);

            var ordersPaid = _uow.OrderRepository.GetAllQueryable($"{nameof(Order.Payments)}")
                .Where(o => !o.IsDeleted && o.OrderDate >= from && o.OrderDate <= to && o.Status == OrderStatus.Paid);

            var revenue = await ordersPaid.SumAsync(o => (decimal?)o.TotalAmount) ?? 0m;
            var ordersCount = await ordersPaid.CountAsync();

            var newUsers = await _uow.AccountRepository.GetAllQueryable()
                .Where(a => !a.IsDeleted && a.CreatedAt >= from && a.CreatedAt <= to)
                .CountAsync();

            var enrolls = await _uow.EnrollmentRepository.GetAllQueryable()
                .Where(e => !e.IsDeleted && e.EnrollmentDate >= from && e.EnrollmentDate <= to)
                .CountAsync();

            var lrs = await _uow.LivestreamRegistrationRepository.GetAllQueryable()
                .Where(r => !r.IsDeleted && r.RegisteredAt >= from && r.RegisteredAt <= to && r.PaymentStatus == "Paid")
                .CountAsync();

            var paidUserCount = await ordersPaid.Select(o => o.AccountId).Distinct().CountAsync();
            var arpu = paidUserCount > 0 ? revenue / paidUserCount : 0m;

            var dto = new KpiSummaryDTO
            {
                RevenuePaid = revenue,
                OrdersPaid = ordersCount,
                NewUsers = newUsers,
                NewEnrollments = enrolls,
                LivestreamRegistrations = lrs,
                ARPU = arpu
            };
            return Result<KpiSummaryDTO>.Success(dto);
        }

        // ========= Time-series =========

        public async Task<Result<RevenueSeriesDTO>> GetRevenueSeriesAsync(DateRangeQuery q)
        {
            var (from, to) = NormalizeRange(q);
            var gb = q.GroupBy;

            var ordersPaid = _uow.OrderRepository.GetAllQueryable()
                .Where(o => !o.IsDeleted && o.OrderDate >= from && o.OrderDate <= to && o.Status == OrderStatus.Paid);

            IQueryable<TimePointDTO> baseQuery =
                gb == GroupBy.Day
                ? ordersPaid
                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month, o.OrderDate.Day })
                    .Select(g => new TimePointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = g.Key.Day, Value = g.Sum(x => x.TotalAmount) })
                : ordersPaid
                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                    .Select(g => new TimePointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = null, Value = g.Sum(x => x.TotalAmount) });

            var points = await baseQuery
                .OrderBy(p => p.Year).ThenBy(p => p.Month).ThenBy(p => p.Day)
                .ToListAsync();

            return Result<RevenueSeriesDTO>.Success(new RevenueSeriesDTO { Points = points });
        }

        public async Task<Result<CountSeriesDTO>> GetOrdersSeriesAsync(DateRangeQuery q)
        {
            var (from, to) = NormalizeRange(q);
            var gb = q.GroupBy;

            var orders = _uow.OrderRepository.GetAllQueryable()
                .Where(o => !o.IsDeleted && o.OrderDate >= from && o.OrderDate <= to);

            IQueryable<CountPointDTO> baseQuery =
                gb == GroupBy.Day
                ? orders.GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month, o.OrderDate.Day })
                        .Select(g => new CountPointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = g.Key.Day, Count = g.Count() })
                : orders.GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                        .Select(g => new CountPointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = null, Count = g.Count() });

            var points = await baseQuery.OrderBy(p => p.Year).ThenBy(p => p.Month).ThenBy(p => p.Day).ToListAsync();
            return Result<CountSeriesDTO>.Success(new CountSeriesDTO { SeriesName = "Orders", Points = points });
        }

        public async Task<Result<CountSeriesDTO>> GetEnrollmentsSeriesAsync(DateRangeQuery q)
        {
            var (from, to) = NormalizeRange(q);
            var gb = q.GroupBy;

            var enrolls = _uow.EnrollmentRepository.GetAllQueryable()
                .Where(e => !e.IsDeleted && e.EnrollmentDate >= from && e.EnrollmentDate <= to);

            IQueryable<CountPointDTO> baseQuery =
                gb == GroupBy.Day
                ? enrolls.GroupBy(e => new { e.EnrollmentDate.Year, e.EnrollmentDate.Month, e.EnrollmentDate.Day })
                        .Select(g => new CountPointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = g.Key.Day, Count = g.Count() })
                : enrolls.GroupBy(e => new { e.EnrollmentDate.Year, e.EnrollmentDate.Month })
                        .Select(g => new CountPointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = null, Count = g.Count() });

            var points = await baseQuery.OrderBy(p => p.Year).ThenBy(p => p.Month).ThenBy(p => p.Day).ToListAsync();
            return Result<CountSeriesDTO>.Success(new CountSeriesDTO { SeriesName = "Enrollments", Points = points });
        }

        public async Task<Result<CountSeriesDTO>> GetUserGrowthSeriesAsync(DateRangeQuery q)
        {
            var (from, to) = NormalizeRange(q);
            var gb = q.GroupBy;

            var users = _uow.AccountRepository.GetAllQueryable()
                .Where(a => !a.IsDeleted && a.CreatedAt >= from && a.CreatedAt <= to);

            IQueryable<CountPointDTO> baseQuery =
                gb == GroupBy.Day
                ? users.GroupBy(a => new { a.CreatedAt.Year, a.CreatedAt.Month, a.CreatedAt.Day })
                      .Select(g => new CountPointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = g.Key.Day, Count = g.Count() })
                : users.GroupBy(a => new { a.CreatedAt.Year, a.CreatedAt.Month })
                      .Select(g => new CountPointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = null, Count = g.Count() });

            var points = await baseQuery.OrderBy(p => p.Year).ThenBy(p => p.Month).ThenBy(p => p.Day).ToListAsync();
            return Result<CountSeriesDTO>.Success(new CountSeriesDTO { SeriesName = "New Users", Points = points });
        }

        public async Task<Result<RevenueSeriesDTO>> GetLivestreamRevenueSeriesAsync(DateRangeQuery q)
        {
            var (from, to) = NormalizeRange(q);
            var gb = q.GroupBy;

            var paidOrders = _uow.OrderRepository.GetAllQueryable($"{nameof(Order.OrderDetails)}")
                .Where(o => !o.IsDeleted && o.OrderDate >= from && o.OrderDate <= to && o.Status == OrderStatus.Paid);

            var details = paidOrders
                .SelectMany(o => o.OrderDetails.Where(d => !d.IsDeleted && d.ItemType == ItemType.Livestream)
                                               .Select(d => new { o.OrderDate, d.Price }));

            IQueryable<TimePointDTO> baseQuery =
                gb == GroupBy.Day
                ? details.GroupBy(x => new { x.OrderDate.Year, x.OrderDate.Month, x.OrderDate.Day })
                         .Select(g => new TimePointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = g.Key.Day, Value = g.Sum(x => x.Price) })
                : details.GroupBy(x => new { x.OrderDate.Year, x.OrderDate.Month })
                         .Select(g => new TimePointDTO { Year = g.Key.Year, Month = g.Key.Month, Day = null, Value = g.Sum(x => x.Price) });

            var points = await baseQuery.OrderBy(p => p.Year).ThenBy(p => p.Month).ThenBy(p => p.Day).ToListAsync();
            return Result<RevenueSeriesDTO>.Success(new RevenueSeriesDTO { Points = points });
        }

        public async Task<PagedResult<TopCourseRevenueItemDTO>> GetTopCoursesByRevenueAsync(DateRangeQuery q, int page, int pageSize)
        {
            var (from, to) = NormalizeRange(q);
            page = Math.Max(1, page);
            pageSize = pageSize is > 0 and <= 100 ? pageSize : 10;

            var paidOrders = _uow.OrderRepository.GetAllQueryable($"{nameof(Order.OrderDetails)}")
                .Where(o => !o.IsDeleted && o.OrderDate >= from && o.OrderDate <= to && o.Status == OrderStatus.Paid);

            var details = paidOrders
                .SelectMany(o => o.OrderDetails.Where(d => !d.IsDeleted && d.ItemType == ItemType.Course)
                                               .Select(d => new { d.ItemId, d.Price }));

            var grouped = details.GroupBy(x => x.ItemId)
                .Select(g => new { CourseId = g.Key, Revenue = g.Sum(x => x.Price), Orders = g.Count() });

            var total = await grouped.CountAsync();

            var pageData = await grouped
                .OrderByDescending(x => x.Revenue)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var courseIds = pageData.Select(x => x.CourseId).Distinct().ToList();
            var courseTitles = await _uow.CourseRepository.GetAllQueryable()
                .Where(c => courseIds.Contains(c.Id))
                .Select(c => new { c.Id, c.Title })
                .ToDictionaryAsync(x => x.Id, x => x.Title);

            var pageDataWithTitles = pageData.Select(x => new TopCourseRevenueItemDTO
            {
                CourseId = x.CourseId,
                Title = courseTitles.TryGetValue(x.CourseId, out var t) ? t : $"Course #{x.CourseId}",
                Revenue = x.Revenue,
                Orders = x.Orders
            }).ToList();

            return PagedResult<TopCourseRevenueItemDTO>.Success(pageDataWithTitles, total, page, pageSize);
        }

        public async Task<PagedResult<TopCourseEnrollmentItemDTO>> GetTopCoursesByEnrollmentsAsync(DateRangeQuery q, int page, int pageSize)
        {
            var (from, to) = NormalizeRange(q);
            page = Math.Max(1, page);
            pageSize = pageSize is > 0 and <= 100 ? pageSize : 10;

            var enrolls = _uow.EnrollmentRepository.GetAllQueryable($"{nameof(Enrollment.Course)}")
                .Where(e => !e.IsDeleted && e.EnrollmentDate >= from && e.EnrollmentDate <= to);

            var grouped = enrolls.GroupBy(e => new { e.CourseId, e.Course.Title })
                .Select(g => new { g.Key.CourseId, g.Key.Title, Enrollments = g.Count() });

            var total = await grouped.CountAsync();

            var pageData = await grouped
                .OrderByDescending(x => x.Enrollments)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TopCourseEnrollmentItemDTO
                {
                    CourseId = x.CourseId,
                    Title = x.Title,
                    Enrollments = x.Enrollments
                })
                .ToListAsync();

            return PagedResult<TopCourseEnrollmentItemDTO>.Success(pageData, total, page, pageSize);
        }

        public async Task<PagedResult<TopLivestreamRevenueItemDTO>> GetTopLivestreamsByRevenueAsync(DateRangeQuery q, int page, int pageSize)
        {
            var (from, to) = NormalizeRange(q);
            page = Math.Max(1, page);
            pageSize = pageSize is > 0 and <= 100 ? pageSize : 10;

            var paidOrders = _uow.OrderRepository.GetAllQueryable($"{nameof(Order.OrderDetails)}")
                .Where(o => !o.IsDeleted && o.OrderDate >= from && o.OrderDate <= to && o.Status == OrderStatus.Paid);

            var details = paidOrders
                .SelectMany(o => o.OrderDetails.Where(d => !d.IsDeleted && d.ItemType == ItemType.Livestream)
                                               .Select(d => new { d.ItemId, d.Price }));

            var grouped = details.GroupBy(x => x.ItemId)
                .Select(g => new { LivestreamId = g.Key, Revenue = g.Sum(x => x.Price), Orders = g.Count() });

            var total = await grouped.CountAsync();

            var pageData = await grouped
                .OrderByDescending(x => x.Revenue)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var livestreamIds = pageData.Select(x => x.LivestreamId).Distinct().ToList();
            var livestreamTitles = await _uow.LivestreamRepository.GetAllQueryable()
                .Where(l => livestreamIds.Contains(l.Id))
                .Select(l => new { l.Id, l.Title })
                .ToDictionaryAsync(x => x.Id, x => x.Title);

            var pageDataWithTitles = pageData.Select(x => new TopLivestreamRevenueItemDTO
            {
                LivestreamId = x.LivestreamId,
                Title = livestreamTitles.TryGetValue(x.LivestreamId, out var t) ? t : $"Livestream #{x.LivestreamId}",
                Revenue = x.Revenue,
                Orders = x.Orders
            }).ToList();

            return PagedResult<TopLivestreamRevenueItemDTO>.Success(pageDataWithTitles, total, page, pageSize);
        }
    }
}

