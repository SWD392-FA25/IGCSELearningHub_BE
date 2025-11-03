using System.Linq;
using Application.Exceptions;
using Application.Payments.DTOs;
using Application.Payments.Interfaces;
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
    [Authorize]
    public sealed class AdminOrdersController : ControllerBase
    {
        private readonly IEnrollmentAdminService _enrollmentAdminService;
        private readonly ICashPaymentService _cashPaymentService;

        public AdminOrdersController(IEnrollmentAdminService enrollmentAdminService, ICashPaymentService cashPaymentService)
        {
            _enrollmentAdminService = enrollmentAdminService;
            _cashPaymentService = cashPaymentService;
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

        /// <summary>
        /// Ghi nhận thanh toán tiền mặt cho đơn hàng.
        /// </summary>
        [HttpPost("{orderId:int}/cash-payment")]
        public async Task<IActionResult> CashPayment([FromRoute] int orderId, [FromBody] CashPaymentRequestDTO? request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");
                throw new ValidationException(errors);
            }

            request ??= new CashPaymentRequestDTO();
            var result = await _cashPaymentService.ProcessAsync(orderId, request);
            return StatusCode(result.StatusCode, result);
        }
    }
}
