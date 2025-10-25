using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Quiz
{
    public class QuizDetailDTO : QuizSummaryDTO
    {
        public bool RandomizeQuestions { get; set; }
        public bool RandomizeOptions { get; set; }
        public List<QuizDetailQuestionDTO> Questions { get; set; } = new();
    }
}
