using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.QuizTaking
{
    public class SubmitAttemptDTO
    {
        public List<SubmitAnswerItem> Answers { get; set; } = new();
    }
}
