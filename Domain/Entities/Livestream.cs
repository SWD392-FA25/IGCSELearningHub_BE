using Domain.Common;

namespace Domain.Entities;

public partial class Livestream : BaseFullEntity
{
    public int CourseId { get; set; }

    public int TeacherId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Schedule { get; set; }

    public decimal Price { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<LivestreamRegistration> LivestreamRegistrations { get; set; } = new List<LivestreamRegistration>();

    public virtual Account Teacher { get; set; } = null!;
}
