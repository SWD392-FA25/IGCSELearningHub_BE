using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.QuizTaking
{
    public class SubmitAnswerItem
    {
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; } // cho MCQ là OptionId; câu khác có thể null
    }
}
