using Domain.Common;

namespace Domain.Entities;

public partial class Progress : BaseFullEntity
{
    public int EnrollmentId { get; set; }

    public byte CompletedPercent { get; set; }

    public DateTime? LastAccessDate { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;
}
