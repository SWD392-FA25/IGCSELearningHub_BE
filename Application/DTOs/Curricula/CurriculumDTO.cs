namespace Application.DTOs.Curricula
{
    public class CurriculumDTO
    {
        public int CurriculumId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
    }
}
