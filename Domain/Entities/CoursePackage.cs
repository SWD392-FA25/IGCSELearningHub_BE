using Domain.Common;

namespace Domain.Entities;

public partial class CoursePackage : BaseFullEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
