using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notifications
{
    public interface IPushNotificationService
    {
        Task SendPaymentSuccessAsync(int accountId, int orderId, IEnumerable<string> deviceTokens, CancellationToken ct = default);
    }
}
