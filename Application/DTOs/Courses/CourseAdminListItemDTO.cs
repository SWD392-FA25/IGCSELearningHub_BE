using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Courses
{
    public class CourseAdminListItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Level { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
