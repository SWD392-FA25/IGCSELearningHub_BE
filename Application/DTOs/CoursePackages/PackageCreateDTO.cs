using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CoursePackages
{
    public class PackageCreateDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<int>? CourseIds { get; set; } // optional: add courses ngay khi tạo
    }
}
