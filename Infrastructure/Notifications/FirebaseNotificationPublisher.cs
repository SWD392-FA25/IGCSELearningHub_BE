using Application.Notifications;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public class FirebaseNotificationPublisher : IPushNotificationService
    {
        private readonly FirebaseMessaging _messaging;
        private readonly ILogger<FirebaseNotificationPublisher> _logger;

        public FirebaseNotificationPublisher(
            FirebaseMessaging messaging,
            ILogger<FirebaseNotificationPublisher> logger)
        {
            _messaging = messaging;
            _logger = logger;
        }

        public async Task SendPaymentSuccessAsync(int accountId, int orderId, IEnumerable<string> deviceTokens, CancellationToken ct = default)
        {
            var tokens = deviceTokens.Where(t => !string.IsNullOrWhiteSpace(t)).Distinct().ToList();

            if (!tokens.Any())
            {
                _logger.LogDebug("No active device tokens for account {AccountId}", accountId);
                return;
            }

            var payload = new Dictionary<string, string>
            {
                ["type"] = "payment_success",
                ["orderId"] = orderId.ToString()
            };

            var multicast = new MulticastMessage
            {
                Tokens = tokens,
                Notification = new Notification
                {
                    Title = "Thanh toán thành công",
                    Body = "Khóa học đã được mở. Sẵn sàng học ngay!"
                },
                Data = payload
            };

            await SendMulticastAsync(accountId, "payment_success", tokens, multicast, ct);
        }

        private async Task SendMulticastAsync(int accountId, string type, IReadOnlyList<string> tokens, MulticastMessage message, CancellationToken ct)
        {
            try
            {
                var response = await _messaging.SendMulticastAsync(message, ct);

                for (var i = 0; i < response.Responses.Count; i++)
                {
                    var token = tokens[i];
                    var item = response.Responses[i];

                    if (item.IsSuccess)
                    {
                        _logger.LogInformation(
                            "FCM sent. UserId={UserId}, Token={TokenMasked}, MsgId={MsgId}, Type={Type}",
                            accountId,
                            MaskToken(token),
                            item.MessageId,
                            type);
                    }
                    else
                    {
                        _logger.LogWarning(item.Exception,
                            "FCM failed. UserId={UserId}, Token={TokenMasked}, ErrorCode={ErrorCode}, Type={Type}",
                            accountId,
                            MaskToken(token),
                            item.Exception?.ErrorCode,
                            type);
                    }
                }
            }
            catch (FirebaseMessagingException ex)
            {
                _logger.LogWarning(ex,
                    "FCM multicast error. UserId={UserId}, Type={Type}, ErrorCode={ErrorCode}",
                    accountId,
                    type,
                    ex.ErrorCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "FCM send error (unexpected). UserId={UserId}, Type={Type}",
                    accountId,
                    type);
            }
        }

        private static string MaskToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return string.Empty;
            if (token.Length <= 6) return "***";
            return token[..3] + "***" + token[^3..];
        }
    }
}
