using Application.Payments.DTOs;
using Application.Payments.Interfaces;
using Application.Wrappers;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/vnpay")]
    public class VnPayController : ControllerBase
    {
        private readonly IPaymentOrchestrator _orchestrator;

        public VnPayController(IPaymentOrchestrator orchestrator) => _orchestrator = orchestrator;

        /// <summary>
        /// [Internal] Tạo URL thanh toán VNPay cho một đơn hàng.
        /// Khuyến nghị client dùng endpoint tiện lợi: POST /api/v1/me/orders/{orderId}/checkout
        /// </summary>
        [HttpPost("checkout")]
        [Authorize(Roles = "Admin,Teacher")] // giới hạn nội bộ/test/ops
        [ApiExplorerSettings(IgnoreApi = true)] // ẩn khỏi Swagger công khai
        public async Task<IActionResult> CreateCheckout([FromBody] CreatePaymentCommand cmd, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(cmd.ClientIp))
            {
                // fallback IP
                cmd.ClientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            }

            var url = await _orchestrator.CreateCheckoutAsync(cmd, ct);
            var res = Result<PaymentCheckoutDTO>.Success(url, "Checkout URL generated.", 200);
            return StatusCode(res.StatusCode, res);
        }

        /// <summary>
        /// Endpoint VNPay gọi về sau khi thanh toán (ReturnUrl)
        /// </summary>
        [HttpGet("callback")]
        [AllowAnonymous] // VNPay sẽ gọi public
        public async Task<IActionResult> VnPayCallback(CancellationToken ct)
        {
            var result = await _orchestrator.HandleCallbackAsync(Request.Query, ct);

            // Trả theo wrapper chuẩn
            var res = Result<PaymentResultDTO>.Success(result, result.IsSuccess ? "Payment success" : "Payment failed", 200)
                .AddDetail("provider", "VNPay");
            return StatusCode(res.StatusCode, res);
        }
    }
}
