namespace Application.DTOs.Curricula
{
    public class CurriculumUpdateDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? OrderIndex { get; set; }
    }
}
