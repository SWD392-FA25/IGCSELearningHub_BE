using Application.DTOs.Analytics;
using Application.Extensions;
using Application.Services.Interfaces;
using Application.Wrappers;
using Application.Utils.Interfaces;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .Select(g => new CourseRevenueAggregate(g.Key, g.Sum(x => x.Price), g.Count()));

            var ordered = grouped.OrderByDescending(x => x.Revenue);
            var pageResult = await ordered.ToPagedResultAsync(page, pageSize);
            var pageData = pageResult.Data?.ToList() ?? new List<CourseRevenueAggregate>();

            var mapped = await QueryLookupHelper.MapWithTitlesAsync(
                pageData,
                _uow.CourseRepository.GetAllQueryable(),
                agg => agg.CourseId,
                c => c.Id,
                c => c.Title,
                (agg, title) => new TopCourseRevenueItemDTO
                {
                    CourseId = agg.CourseId,
                    Title = title,
                    Revenue = agg.Revenue,
                    Orders = agg.Orders
                },
                "Course #{0}");

            return PagedResult<TopCourseRevenueItemDTO>.Success(
                mapped,
                pageResult.TotalCount,
                pageResult.PageNumber,
                pageResult.PageSize);
        }

        public async Task<PagedResult<TopCourseEnrollmentItemDTO>> GetTopCoursesByEnrollmentsAsync(DateRangeQuery q, int page, int pageSize)
        {
            var (from, to) = NormalizeRange(q);
            page = Math.Max(1, page);
            pageSize = pageSize is > 0 and <= 100 ? pageSize : 10;

            var enrolls = _uow.EnrollmentRepository.GetAllQueryable($"{nameof(Enrollment.Course)}")
                .Where(e => !e.IsDeleted && e.EnrollmentDate >= from && e.EnrollmentDate <= to);

            var grouped = enrolls.GroupBy(e => new { e.CourseId, e.Course.Title })
                .Select(g => new CourseEnrollmentAggregate(g.Key.CourseId, g.Key.Title, g.Count()));

            var ordered = grouped.OrderByDescending(x => x.Enrollments);
            return await ordered.ToPagedResultAsync(
                page,
                pageSize,
                x => new TopCourseEnrollmentItemDTO
                {
                    CourseId = x.CourseId,
                    Title = x.Title,
                    Enrollments = x.Enrollments
                });
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
                .Select(g => new LivestreamRevenueAggregate(g.Key, g.Sum(x => x.Price), g.Count()));

            var ordered = grouped.OrderByDescending(x => x.Revenue);
            var pageResult = await ordered.ToPagedResultAsync(page, pageSize);
            var pageData = pageResult.Data?.ToList() ?? new List<LivestreamRevenueAggregate>();

            var mapped = await QueryLookupHelper.MapWithTitlesAsync(
                pageData,
                _uow.LivestreamRepository.GetAllQueryable(),
                agg => agg.LivestreamId,
                l => l.Id,
                l => l.Title,
                (agg, title) => new TopLivestreamRevenueItemDTO
                {
                    LivestreamId = agg.LivestreamId,
                    Title = title,
                    Revenue = agg.Revenue,
                    Orders = agg.Orders
                },
                "Livestream #{0}");

            return PagedResult<TopLivestreamRevenueItemDTO>.Success(
                mapped,
                pageResult.TotalCount,
                pageResult.PageNumber,
                pageResult.PageSize);
        }

        private sealed record CourseRevenueAggregate(int CourseId, decimal Revenue, int Orders);
        private sealed record CourseEnrollmentAggregate(int CourseId, string Title, int Enrollments);
        private sealed record LivestreamRevenueAggregate(int LivestreamId, decimal Revenue, int Orders);
    }
}
