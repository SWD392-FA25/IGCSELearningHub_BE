namespace Application.DTOs.Livestreams
{
    public class LivestreamPublicListItemDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Schedule { get; set; }
        public decimal Price { get; set; }
    }
}

