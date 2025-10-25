using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Livestreams
{
    public class LivestreamAdminListItemDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Schedule { get; set; }
        public decimal Price { get; set; }
        public int RegistrationCount { get; set; }
    }
}
