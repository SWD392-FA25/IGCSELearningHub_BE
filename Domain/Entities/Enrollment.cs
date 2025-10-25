using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public partial class Enrollment : BaseFullEntity
{
    public int AccountId { get; set; }

    public int CourseId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();

    public virtual Account Account { get; set; } = null!;
}
