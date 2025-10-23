using Application;
using Application.Payments.DTOs;
using Application.Payments.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Payments.Providers.VnPay;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Payments.Providers
{
    public sealed class PaymentOrchestrator : IPaymentOrchestrator
    {
        private readonly IHttpContextAccessor _http;
        private readonly IPaymentGateway _gateway;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<PaymentOrchestrator> _logger;
        private readonly IEnrollmentAdminService? _enrollmentService;

        public PaymentOrchestrator(
            IHttpContextAccessor http,
            IPaymentGateway gateway,
            IUnitOfWork uow,
            ILogger<PaymentOrchestrator> logger,
            IEnrollmentAdminService? enrollmentService = null)
        {
            _http = http;
            _gateway = gateway;
            _uow = uow;
            _logger = logger;
            _enrollmentService = enrollmentService;
        }

        public async Task<PaymentCheckoutDTO> CreateCheckoutAsync(CreatePaymentCommand command, CancellationToken ct = default)
        {
            var order = await _uow.OrderRepository.GetByIdAsync(command.OrderId);
            if (order is null) throw new InvalidOperationException("Order not found.");

            var amountVnd = order.TotalAmount;
            if (amountVnd <= 0) throw new InvalidOperationException("Invalid order amount.");

            var method = await EnsureVnPayMethodAsync(ct);
            var payment = new Payment
            {
                OrderId = order.Id,
                PaymentMethodId = method.Id,
                Amount = amountVnd,
                Status = PaymentStatus.Pending
            };
            await _uow.PaymentRepository.AddAsync(payment);

            var ip = command.ClientIp;
            try
            {
                var reqIp = _http.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                if (!string.IsNullOrWhiteSpace(reqIp)) ip = reqIp;
            }
            catch { /* fallback sang command.ClientIp */ }

            var vnGateway = _gateway as VnPayGateway
                ?? throw new InvalidOperationException("VNPay gateway required.");

            var checkout = await vnGateway.CreateCheckoutUrlInternalAsync(
                orderId: order.Id,
                amountVnd: amountVnd,
                clientIp: ip,
                bankCode: command.BankCode,
                orderDesc: command.OrderDescription,
                orderTypeCode: command.OrderTypeCode,
                ct: ct
            );

            await _uow.SaveChangesAsync();
            return checkout;
        }

        public async Task<PaymentResultDTO> HandleCallbackAsync(IQueryCollection query, CancellationToken ct = default)
        {
            var result = await _gateway.ParseAndValidateCallbackAsync(query, ct);

            var order = await _uow.OrderRepository.GetByIdAsync(result.OrderId);
            if (order is null)
            {
                _logger.LogWarning("Callback for unknown order: {OrderId}", result.OrderId);
                return result;
            }

            var payment = (await _uow.PaymentRepository.GetAllAsync())
                .Where(p => p.OrderId == order.Id && p.Status == PaymentStatus.Pending && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefault();

            if (payment is null)
            {
                var method = await EnsureVnPayMethodAsync(ct);
                payment = new Payment
                {
                    OrderId = order.Id,
                    PaymentMethodId = method.Id,
                    Amount = result.Amount,
                    Status = PaymentStatus.Pending
                };
                await _uow.PaymentRepository.AddAsync(payment);
            }

            if (result.IsSuccess)
            {
                payment.Status = PaymentStatus.Paid;
                payment.PaidDate = DateTime.UtcNow;
                order.Status = OrderStatus.Paid;

                await _uow.SaveChangesAsync();

                if (_enrollmentService != null)
                {
                    try
                    {
                        await _enrollmentService.CreateFromOrderAsync(order.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Auto-enrollment failed for order {OrderId}", order.Id);
                    }
                }
            }
            else
            {
                payment.Status = PaymentStatus.Canceled;
                await _uow.SaveChangesAsync();
            }

            return result;
        }

        private async Task<PaymentMethod> EnsureVnPayMethodAsync(CancellationToken ct)
        {
            var pm = await _uow.PaymentMethodRepository.FindOneAsync(p => p.PaymentMethodName == "VNPay");
            if (pm is null)
            {
                pm = new PaymentMethod
                {
                    PaymentMethodName = "VNPay",
                    PaymentMethodDescription = "VNPay payment gateway",
                    IsActive = true
                };
                await _uow.PaymentMethodRepository.AddAsync(pm);
                await _uow.SaveChangesAsync();
            }
            return pm;
        }
    }
}
