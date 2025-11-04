using Domain.Common;

namespace Domain.Entities;

public partial class Course : BaseFullEntity
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Level { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Livestream> Livestreams { get; set; } = new List<Livestream>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<CoursePackage> Packages { get; set; } = new List<CoursePackage>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
