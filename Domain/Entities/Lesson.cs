using Domain.Common;

namespace Domain.Entities;

public partial class Lesson : BaseFullEntity
{
    public int CurriculumId { get; set; }
    public Curriculum Curriculum { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? VideoUrl { get; set; }
    public string? AttachmentUrl { get; set; }
    public int OrderIndex { get; set; }

    public bool IsFreePreview { get; set; }
}
