using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.QuizTaking
{
    public class AttemptSummaryDTO
    {
        public int AttemptId { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; } = null!;
        public decimal? Score { get; set; }
        public decimal MaxScore { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}
