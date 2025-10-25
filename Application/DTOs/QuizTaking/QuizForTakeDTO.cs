using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.QuizTaking
{
    public class QuizForTakeDTO
    {
        public int QuizId { get; set; }
        public string Title { get; set; } = null!;
        public int TotalQuestions { get; set; }
        public List<TakeQuestionDTO> Questions { get; set; } = new();
    }
}
