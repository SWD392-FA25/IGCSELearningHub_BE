using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Courses
{
    public class CourseDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Info { get; set; }
        public string? Level { get; set; }
        public string SubjectGroup { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int TotalQuizzes { get; set; }
        public int TotalAssignments { get; set; }
        public int TotalLivestreams { get; set; }
        public IList<CourseCurriculumOutlineDTO> Curricula { get; set; } = new List<CourseCurriculumOutlineDTO>();
    }
}
