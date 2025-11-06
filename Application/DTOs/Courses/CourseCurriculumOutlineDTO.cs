using System.Collections.Generic;

namespace Application.DTOs.Courses
{
    public class CourseCurriculumOutlineDTO
    {
        public int CurriculumId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public IList<CourseLessonOutlineDTO> Lessons { get; set; } = new List<CourseLessonOutlineDTO>();
    }

    public class CourseLessonOutlineDTO
    {
        public int LessonId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsFreePreview { get; set; }
        public string? VideoUrl { get; set; }
        public string? AttachmentUrl { get; set; }
    }
}
