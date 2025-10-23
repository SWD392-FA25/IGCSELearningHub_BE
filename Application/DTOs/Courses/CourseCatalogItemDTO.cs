using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Courses
{
    public class CourseCatalogItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Level { get; set; }
        public decimal Price { get; set; }
        public string? ShortDescription { get; set; }
        public int TotalQuizzes { get; set; }
        public int TotalAssignments { get; set; }
    }
}
