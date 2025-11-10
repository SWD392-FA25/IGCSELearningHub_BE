using System.Threading;
using System.Threading.Tasks;

namespace Application.Notifications
{
    public interface IPaymentRealtimeNotifier
    {
        Task NotifyPaymentSuccessAsync(int accountId, int orderId, CancellationToken ct = default);
        Task NotifyPaymentFailedAsync(int accountId, int orderId, string reason, CancellationToken ct = default);
    }
}
