namespace Application.DTOs.Units
{
    public class UnitUpdateDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? OrderIndex { get; set; }
    }
}
