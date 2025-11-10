using Application.Notifications;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public class NoOpPaymentRealtimeNotifier : IPaymentRealtimeNotifier
    {
        private readonly ILogger<NoOpPaymentRealtimeNotifier> _logger;

        public NoOpPaymentRealtimeNotifier(ILogger<NoOpPaymentRealtimeNotifier> logger)
        {
            _logger = logger;
        }

        public Task NotifyPaymentSuccessAsync(int accountId, int orderId, CancellationToken ct = default)
        {
            _logger.LogDebug("[SignalR placeholder] payment success for account {AccountId}, order {OrderId}", accountId, orderId);
            return Task.CompletedTask;
        }

        public Task NotifyPaymentFailedAsync(int accountId, int orderId, string reason, CancellationToken ct = default)
        {
            _logger.LogDebug("[SignalR placeholder] payment failed for account {AccountId}, order {OrderId}, reason {Reason}", accountId, orderId, reason);
            return Task.CompletedTask;
        }
    }
}
