using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CoursePackages
{
    public class PackageAdminListItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int CourseCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
