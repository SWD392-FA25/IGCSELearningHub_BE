using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Livestreams
{
    public class LivestreamCreateDTO
    {
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Schedule { get; set; }  // đề xuất dùng DateTimeOffset ở entity trong tương lai
        public decimal Price { get; set; }
    }
}
