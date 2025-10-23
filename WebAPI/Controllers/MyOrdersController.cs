using Application.Payments.DTOs;
using Application.Payments.Interfaces;
using Application.Services.Interfaces;
using Application.ViewModels.Orders;
using Application.Wrappers;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/me/orders")]
    [Authorize(Roles = "Student,Admin,Teacher")] // student chính, admin/teacher test được
    public class MyOrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentOrchestrator _pay;
        private readonly IOrderQueryService _orderQuery;

        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public MyOrdersController(IOrderService orderService, IPaymentOrchestrator pay, IOrderQueryService orderQuery)
        {
            _orderService = orderService;
            _pay = pay;
            _orderQuery = orderQuery;
        }

        // POST /me/orders  (tạo đơn từ lựa chọn Course/Package/Livestream)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest req)
        {
            var result = await _orderService.CreateOrderAsync(CurrentUserId, req);
            return StatusCode(result.StatusCode, result);
        }

        // GET /me/orders
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _orderService.GetMyOrdersAsync(CurrentUserId, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        // GET /me/orders/{orderId}
        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> Detail([FromRoute] int orderId)
        {
            var result = await _orderService.GetOrderAsync(CurrentUserId, orderId);
            return StatusCode(result.StatusCode, result);
        }

        // GET /me/orders/{orderId}/status
        [HttpGet("{orderId:int}/status")]
        public async Task<IActionResult> Status([FromRoute] int orderId)
        {
            var result = await _orderQuery.GetOrderStatusAsync(CurrentUserId, orderId);
            return StatusCode(result.StatusCode, result);
        }

        // POST /me/orders/{orderId}/checkout  (tạo VNPay URL nhanh)
        [HttpPost("{orderId:int}/checkout")]
        public async Task<IActionResult> Checkout([FromRoute] int orderId)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            var checkout = await _pay.CreateCheckoutAsync(new CreatePaymentCommand
            {
                OrderId = orderId,
                ClientIp = ip,
                OrderDescription = $"Thanh toan don hang #{orderId}",
                OrderTypeCode = "other"
            });

            var res = Result<PaymentCheckoutDTO>.Success(checkout, "Checkout URL generated.");
            return StatusCode(res.StatusCode, res);
        }

        //   Retry tạo URL thanh toán(khi đơn vẫn chưa Paid)
        // POST /me/orders/{orderId}/retry-checkout
        [HttpPost("{orderId:int}/retry-checkout")]
        public async Task<IActionResult> RetryCheckout([FromRoute] int orderId)
        {
            // Kiểm tra nhanh: chỉ cho retry nếu order của user và chưa Paid
            var status = await _orderQuery.GetOrderStatusAsync(CurrentUserId, orderId);
            if (!status.Succeeded) return StatusCode(status.StatusCode, status);
            if (status.Data!.Status.Equals("Paid", StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(400, Result<object>.Fail("Order already paid.", 400));
            }

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            var checkout = await _pay.CreateCheckoutAsync(new CreatePaymentCommand
            {
                OrderId = orderId,
                ClientIp = ip,
                OrderDescription = $"Thanh toan don hang #{orderId}",
                OrderTypeCode = "other"
            });

            return StatusCode(200, Result<PaymentCheckoutDTO>.Success(checkout, "New checkout URL generated"));
        }
    }
}
