namespace Application.DTOs.Lessons
{
    public class LessonDetailDTO
    {
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public string? AttachmentUrl { get; set; }
        public int OrderIndex { get; set; }
        public bool IsFreePreview { get; set; }
        public bool Completed { get; set; }
    }
}
