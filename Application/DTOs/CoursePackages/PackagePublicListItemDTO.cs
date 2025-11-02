namespace Application.DTOs.CoursePackages
{
    public class PackagePublicListItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int CourseCount { get; set; }
    }
}

