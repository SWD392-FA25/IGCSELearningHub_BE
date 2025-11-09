using Domain.Common;

namespace Domain.Entities;

public class Device : BaseFullEntity
{
    public int AccountId { get; set; }
    public string DeviceToken { get; set; } = null!;
    public string DeviceType { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public string OsVersion { get; set; } = string.Empty;
    public string AppVersion { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime? LastSeenAt { get; set; }

    public virtual Account Account { get; set; } = null!;
}
