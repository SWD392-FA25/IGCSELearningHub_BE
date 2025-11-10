using System.Threading;
using System.Threading.Tasks;

namespace Application.Notifications
{
    public interface IPushNotificationService
    {
        Task SendPaymentSuccessAsync(int accountId, int orderId, CancellationToken ct = default);
    }
}
