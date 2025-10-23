using Application.DTOs.Analytics;
using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/analytics")]
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminAnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _svc;
        public AdminAnalyticsController(IAnalyticsService svc) => _svc = svc;

        // KPIs tổng quan
        [HttpGet("kpis")]
        public async Task<IActionResult> GetKpis([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var res = await _svc.GetKpisAsync(new DateRangeQuery { From = from, To = to });
            return StatusCode(res.StatusCode, res);
        }

        // Time-series: revenue / orders / enrollments / users / livestream revenue
        [HttpGet("revenue-series")]
        public async Task<IActionResult> RevenueSeries([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] GroupBy groupBy = GroupBy.Day)
        {
            var res = await _svc.GetRevenueSeriesAsync(new DateRangeQuery { From = from, To = to, GroupBy = groupBy });
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("orders-series")]
        public async Task<IActionResult> OrdersSeries([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] GroupBy groupBy = GroupBy.Day)
        {
            var res = await _svc.GetOrdersSeriesAsync(new DateRangeQuery { From = from, To = to, GroupBy = groupBy });
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("enrollments-series")]
        public async Task<IActionResult> EnrollmentsSeries([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] GroupBy groupBy = GroupBy.Day)
        {
            var res = await _svc.GetEnrollmentsSeriesAsync(new DateRangeQuery { From = from, To = to, GroupBy = groupBy });
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("users-series")]
        public async Task<IActionResult> UsersSeries([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] GroupBy groupBy = GroupBy.Day)
        {
            var res = await _svc.GetUserGrowthSeriesAsync(new DateRangeQuery { From = from, To = to, GroupBy = groupBy });
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("livestream-revenue-series")]
        public async Task<IActionResult> LivestreamRevenueSeries([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] GroupBy groupBy = GroupBy.Day)
        {
            var res = await _svc.GetLivestreamRevenueSeriesAsync(new DateRangeQuery { From = from, To = to, GroupBy = groupBy });
            return StatusCode(res.StatusCode, res);
        }

        // Top lists
        [HttpGet("top-courses/revenue")]
        public async Task<IActionResult> TopCoursesRevenue([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _svc.GetTopCoursesByRevenueAsync(new DateRangeQuery { From = from, To = to }, pageNumber, pageSize);
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("top-courses/enrollments")]
        public async Task<IActionResult> TopCoursesEnrollments([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _svc.GetTopCoursesByEnrollmentsAsync(new DateRangeQuery { From = from, To = to }, pageNumber, pageSize);
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("top-livestreams/revenue")]
        public async Task<IActionResult> TopLivestreamsRevenue([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await _svc.GetTopLivestreamsByRevenueAsync(new DateRangeQuery { From = from, To = to }, pageNumber, pageSize);
            return StatusCode(res.StatusCode, res);
        }
    }
}