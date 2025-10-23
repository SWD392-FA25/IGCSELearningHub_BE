using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Courses
{
    public class CourseAdminDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Level { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalQuizzes { get; set; }
        public int TotalAssignments { get; set; }
        public int TotalLivestreams { get; set; }
        public int TotalEnrollments { get; set; }
    }
}
