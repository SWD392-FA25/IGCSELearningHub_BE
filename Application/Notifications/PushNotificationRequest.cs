using System.Collections.Generic;

namespace Application.Notifications
{
    public class PushNotificationRequest
    {
        public int UserId { get; set; }
        public string DeviceToken { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string? Type { get; set; }
        public IDictionary<string, string>? Data { get; set; }
    }
}
