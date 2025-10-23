using Domain.Common;

namespace Domain.Entities;

public partial class LivestreamRegistration : BaseFullEntity
{
    public int LivestreamId { get; set; }

    public int AccountId { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime RegisteredAt { get; set; }

    public virtual Livestream Livestream { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;
}
