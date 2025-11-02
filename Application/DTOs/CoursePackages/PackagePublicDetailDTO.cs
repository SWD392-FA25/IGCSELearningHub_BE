using System.Collections.Generic;

namespace Application.DTOs.CoursePackages
{
    public class PackagePublicDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<PackageCourseItemDTO> Courses { get; set; } = new();
    }
}

