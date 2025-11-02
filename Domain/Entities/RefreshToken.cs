using Domain.Common;

namespace Domain.Entities;

public partial class RefreshToken : BaseFullEntity
{
    public int AccountId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByToken { get; set; }
    public string? CreatedByIp { get; set; }
    public string? RevokedByIp { get; set; }

    public bool IsActive => RevokedAt == null && !IsDeleted && ExpiresAt > DateTime.UtcNow;

    public virtual Account Account { get; set; } = null!;
}

