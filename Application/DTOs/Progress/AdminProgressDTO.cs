using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Progress
{
    public class AdminProgressDTO
    {
        public int AccountId { get; set; }
        public string StudentName { get; set; } = null!;
        public int EnrollmentId { get; set; }
        public byte CompletedPercent { get; set; }
        public DateTime? LastAccessDate { get; set; }
    }
}
