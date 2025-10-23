using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/orders")]
    [Authorize(Roles = "Admin,Teacher")]
    public sealed class AdminOrdersController : ControllerBase
    {
        private readonly IEnrollmentAdminService _enrollmentAdminService;

        public AdminOrdersController(IEnrollmentAdminService enrollmentAdminService)
        {
            _enrollmentAdminService = enrollmentAdminService;
        }

        /// <summary>
        /// Re-sync enrollments từ order đã thanh toán (idempotent).
        /// Tạo Enrollment cho các Course (kể cả expand từ Package) và tạo LivestreamRegistration cho Livestream.
        /// Bỏ qua những enrollment/registration đã tồn tại hoặc đơn chưa paid.
        /// </summary>
        [HttpPost("{orderId:int}/enroll-sync")]
        public async Task<IActionResult> EnrollSync([FromRoute] int orderId)
        {
            var result = await _enrollmentAdminService.CreateFromOrderAsync(orderId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
