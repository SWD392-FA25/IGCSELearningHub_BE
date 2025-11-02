namespace Application.DTOs.Livestreams
{
    public class LivestreamPublicDetailDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Schedule { get; set; }
        public decimal Price { get; set; }
    }
}

