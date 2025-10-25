using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Livestreams
{
    public class LivestreamUpdateDTO
    {
        public string Title { get; set; } = null!;
        public DateTime Schedule { get; set; }
        public decimal Price { get; set; }
        public int? TeacherId { get; set; }  // cho phép đổi giáo viên
    }
}
