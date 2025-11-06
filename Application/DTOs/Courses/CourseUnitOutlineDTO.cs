using System.Collections.Generic;

namespace Application.DTOs.Courses
{
    public class CourseUnitOutlineDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Order { get; set; }
        public IList<CourseLessonOutlineDTO> Lessons { get; set; } = new List<CourseLessonOutlineDTO>();
    }
}
