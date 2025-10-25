using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Progress
{
    public class StudentProgressDTO
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public byte CompletedPercent { get; set; }
        public DateTime? LastAccessDate { get; set; }
    }
}
