using Application.Notifications;
using Application.IRepository;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public class FirebasePushNotificationService : IPushNotificationService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly FirebaseMessaging _messaging;
        private readonly ILogger<FirebasePushNotificationService> _logger;

        public FirebasePushNotificationService(IDeviceRepository deviceRepository, FirebaseMessaging messaging, ILogger<FirebasePushNotificationService> logger)
        {
            _deviceRepository = deviceRepository;
            _messaging = messaging;
            _logger = logger;
        }

        public async Task SendPaymentSuccessAsync(int accountId, int orderId, CancellationToken ct = default)
        {
            var tokens = await _deviceRepository.GetAllQueryable()
                .Where(d => d.AccountId == accountId && d.IsActive && !string.IsNullOrWhiteSpace(d.DeviceToken))
                .Select(d => d.DeviceToken)
                .ToListAsync(ct);

            if (!tokens.Any())
            {
                _logger.LogDebug("No active device tokens for account {AccountId}", accountId);
                return;
            }

            var message = new MulticastMessage
            {
                Tokens = tokens,
                Notification = new Notification
                {
                    Title = "Thanh toán thành công",
                    Body = "Khoá học đã được mở. Sẵn sàng học ngay!"
                },
                Data = new Dictionary<string, string>
                {
                    ["type"] = "payment_success",
                    ["orderId"] = orderId.ToString()
                }
            };

            try
            {
                await _messaging.SendMulticastAsync(message, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send payment push notification to account {AccountId}", accountId);
            }
        }
    }
}
