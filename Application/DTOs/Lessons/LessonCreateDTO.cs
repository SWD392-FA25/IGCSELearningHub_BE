namespace Application.DTOs.Lessons
{
    public class LessonCreateDTO
    {
        public int UnitId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public string? AttachmentUrl { get; set; }
        public bool? IsFreePreview { get; set; }
    }
}
