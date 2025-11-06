namespace Application.DTOs.Curricula
{
    public class CurriculumCreateDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? OrderIndex { get; set; }
    }
}
