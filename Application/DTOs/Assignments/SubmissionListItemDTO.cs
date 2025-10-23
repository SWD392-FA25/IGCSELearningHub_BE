using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Assignments
{
    public class SubmissionListItemDTO
    {
        public int SubmissionId { get; set; }
        public int AccountId { get; set; }
        public string? AccountUserName { get; set; }
        public decimal? Score { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
