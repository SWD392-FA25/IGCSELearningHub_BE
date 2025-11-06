using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Courses
{
    public class CourseLessonOutlineDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Order { get; set; }
        public bool IsFreePreview { get; set; }
        public bool Completed { get; set; }
        public string? VideoUrl { get; set; }
        public string? AttachmentUrl { get; set; }
    }
}
