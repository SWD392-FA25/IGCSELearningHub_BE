using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Progress
{
    public class ProgressDTO
    {
        public int ProgressId { get; set; }
        public int EnrollmentId { get; set; }
        public byte CompletedPercent { get; set; }
        public DateTime? LastAccessDate { get; set; }
    }
}
