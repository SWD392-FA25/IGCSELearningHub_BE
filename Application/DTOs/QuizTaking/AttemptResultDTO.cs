using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.QuizTaking
{
    public class AttemptResultDTO
    {
        public int AttemptId { get; set; }
        public int QuizId { get; set; }
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }
        public DateTime AttemptDate { get; set; }
        public List<AttemptResultDetail> Details { get; set; } = new();
    }
}
