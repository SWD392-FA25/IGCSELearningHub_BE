using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Packages
{
    public class PackageCourseItemDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Level { get; set; }
        public decimal Price { get; set; }
    }
}
