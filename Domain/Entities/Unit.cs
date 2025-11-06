using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Unit : BaseFullEntity
{
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
