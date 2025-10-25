using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Submissions
{
    public class SubmissionAdminListItemDto
    {
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public int AccountId { get; set; }
        public string StudentName { get; set; } = null!;
        public DateTime SubmittedDate { get; set; }
        public decimal? Score { get; set; }
        public string? AttachmentUrl { get; set; }
    }
}
