namespace Application.DTOs.Units
{
    public class UnitCreateDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? OrderIndex { get; set; }
    }
}
