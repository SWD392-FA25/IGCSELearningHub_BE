using Application.DTOs.Analytics;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<Result<KpiSummaryDto>> GetKpisAsync(DateRangeQuery q);

        Task<Result<RevenueSeriesDto>> GetRevenueSeriesAsync(DateRangeQuery q);
        Task<Result<CountSeriesDto>> GetOrdersSeriesAsync(DateRangeQuery q);
        Task<Result<CountSeriesDto>> GetEnrollmentsSeriesAsync(DateRangeQuery q);
        Task<Result<CountSeriesDto>> GetUserGrowthSeriesAsync(DateRangeQuery q);
        Task<Result<RevenueSeriesDto>> GetLivestreamRevenueSeriesAsync(DateRangeQuery q);

        Task<PagedResult<TopCourseRevenueItemDto>> GetTopCoursesByRevenueAsync(DateRangeQuery q, int page, int pageSize);
        Task<PagedResult<TopCourseEnrollmentItemDto>> GetTopCoursesByEnrollmentsAsync(DateRangeQuery q, int page, int pageSize);
        Task<PagedResult<TopLivestreamRevenueItemDto>> GetTopLivestreamsByRevenueAsync(DateRangeQuery q, int page, int pageSize);
    }
}
