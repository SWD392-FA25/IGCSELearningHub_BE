using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Packages
{
    public class PackageUpdateDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<int>? CourseIds { get; set; } // optional: replace-all khi cập nhật
        public bool ReplaceAllCourses { get; set; } = false; // true -> thay toàn bộ
    }
}
