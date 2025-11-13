using Application;
using Application.Notifications;
using Application.Payments.DTOs;
using Application.Payments.Helpers;
using Application.Payments.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Payments.Providers.VnPay;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Payments.Providers
{
    public sealed class PaymentOrchestrator : IPaymentOrchestrator
    {
        private readonly IHttpContextAccessor _http;
        private readonly IPaymentGateway _gateway;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<PaymentOrchestrator> _logger;
        private readonly IEnrollmentAdminService? _enrollmentService;
        private readonly IPushNotificationService _pushNotifications;
        private readonly IPaymentRealtimeNotifier _realtimeNotifier;
        private readonly IDeviceService _deviceService;

        public PaymentOrchestrator(
            IHttpContextAccessor http,
            IPaymentGateway gateway,
            IUnitOfWork uow,
            ILogger<PaymentOrchestrator> logger,
            IPushNotificationService pushNotifications,
            IPaymentRealtimeNotifier realtimeNotifier,
            IDeviceService deviceService,
            IEnrollmentAdminService? enrollmentService = null)
        {
            _http = http;
            _gateway = gateway;
            _uow = uow;
            _logger = logger;
            _enrollmentService = enrollmentService;
            _pushNotifications = pushNotifications;
            _realtimeNotifier = realtimeNotifier;
            _deviceService = deviceService;
        }

        public async Task<PaymentCheckoutDTO> CreateCheckoutAsync(CreatePaymentCommand command, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            var order = await _uow.OrderRepository.GetByIdAsync(command.OrderId);
            if (order is null) throw new InvalidOperationException("OrderIndex not found.");

            var amountVnd = order.TotalAmount;
            if (amountVnd <= 0) throw new InvalidOperationException("Invalid order amount.");

            var method = await EnsureVnPayMethodAsync(ct);

            var ip = command.ClientIp;
            try
            {
                var reqIp = _http.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                if (!string.IsNullOrWhiteSpace(reqIp)) ip = reqIp;
            }
            catch { /* fallback sang command.ClientIp */ }

            var vnGateway = _gateway as VnPayPaymentGateway
                ?? throw new InvalidOperationException("VNPay gateway required.");

            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                var payment = new Payment
                {
                    OrderId = order.Id,
                    PaymentMethodId = method.Id,
                    Amount = amountVnd,
                    Status = PaymentStatus.Pending,
                    Channel = command.Channel
                };
                await _uow.PaymentRepository.AddAsync(payment);
                await _uow.SaveChangesAsync();

                var checkout = await vnGateway.CreateCheckoutUrlInternalAsync(
                    orderId: order.Id,
                    amountVnd: amountVnd,
                    clientIp: ip,
                    bankCode: command.BankCode,
                    orderDesc: command.OrderDescription,
                    orderTypeCode: command.OrderTypeCode,
                    ct: ct
                );

                await transaction.CommitAsync();
                return checkout;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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

            var alreadyPaid = await _uow.PaymentRepository
                .GetAllQueryable()
                .AnyAsync(p => p.OrderId == order.Id && p.Status == PaymentStatus.Paid, ct)
                || order.Status == OrderStatus.Paid;

            if (alreadyPaid)
            {
                _logger.LogInformation("Idempotent VNPay callback for already-paid order {OrderId}. Ignored.", order.Id);
                result.IsSuccess = true;
                result.Status = PaymentStatus.Paid;
                result.Message = (result.Message ?? "").Length > 0 ? result.Message : "OrderIndex already paid. Callback ignored.";
                result.Channel = await GetLatestPaymentChannelAsync(order.Id, ct);
                return result;
            }

            var pendingPayment = await _uow.PaymentRepository
                .GetAllQueryable()
                .Where(p => p.OrderId == order.Id && p.Status == PaymentStatus.Pending && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync(ct);

            if (!result.IsSuccess)
            {
                if (pendingPayment != null)
                {
                    await using var failureTx = await _uow.BeginTransactionAsync();
                    try
                    {
                        pendingPayment.Status = PaymentStatus.Canceled;
                        _uow.PaymentRepository.Update(pendingPayment);
                        await _uow.SaveChangesAsync();
                        await failureTx.CommitAsync();
                    }
                    catch
                    {
                        await failureTx.RollbackAsync();
                        throw;
                    }
                }
                else
                {
                    _logger.LogWarning("VNPay callback failed for order {OrderId} with no pending payment. Ignored.", order.Id);
                }
                await _realtimeNotifier.NotifyPaymentFailedAsync(order.AccountId, order.Id, result.Message ?? "Payment failed", ct);
                result.Channel = pendingPayment?.Channel ?? result.Channel;
                return result;
            }

            var payment = pendingPayment;

            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                // Success path
                if (payment is null)
                {
                    var method = await EnsureVnPayMethodAsync(ct);
                    payment = new Payment
                    {
                        OrderId = order.Id,
                        PaymentMethodId = method.Id,
                        Amount = result.Amount,
                        Status = PaymentStatus.Pending,
                        Channel = PaymentChannel.Web
                    };
                    await _uow.PaymentRepository.AddAsync(payment);
                }

                payment.Status = PaymentStatus.Paid;
                payment.PaidDate = DateTime.UtcNow;
                _uow.PaymentRepository.Update(payment);
                order.Status = OrderStatus.Paid;
                _uow.OrderRepository.Update(order);

                await _uow.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

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

            await _realtimeNotifier.NotifyPaymentSuccessAsync(order.AccountId, order.Id, ct);

            var tokens = await _deviceService.GetActiveDeviceTokensAsync(order.AccountId, ct);
            if (tokens.Count > 0)
            {
                await _pushNotifications.SendPaymentSuccessAsync(order.AccountId, order.Id, tokens, ct);
            }

            result.Channel = payment?.Channel ?? PaymentChannel.Web;
            return result;
        }

        private async Task<PaymentChannel> GetLatestPaymentChannelAsync(int orderId, CancellationToken ct)
        {
            var channel = await _uow.PaymentRepository
                .GetAllQueryable()
                .Where(p => p.OrderId == orderId && !p.IsDeleted)
                .OrderByDescending(p => p.ModifiedAt)
                .Select(p => p.Channel)
                .FirstOrDefaultAsync(ct);

            return channel;
        }

        private async Task<PaymentMethod> EnsureVnPayMethodAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var method = await PaymentMethodUtilities.EnsureMethodAsync(
                _uow,
                PaymentMethodUtilities.VnPayMethodName,
                "VNPay payment gateway",
                true,
                _logger);

            if (!method.IsActive)
            {
                throw new InvalidOperationException("VNPay payment method is currently inactive.");
            }

            return method;
        }
    }
}
