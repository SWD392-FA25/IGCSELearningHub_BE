using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Courses
{
    public class CourseCreateDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Info { get; set; }
        public string? Level { get; set; }
        public decimal Price { get; set; }
        public string SubjectGroup { get; set; } = string.Empty;
    }
}
