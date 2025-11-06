using Domain.Common;

namespace Domain.Entities;

public partial class Assignment : BaseFullEntity
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
