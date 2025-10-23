using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Quiz
{
    public class QuizQuestionInlineDTO
    {
        public string Stem { get; set; } = null!;
        public string? Explanation { get; set; }
        public byte? Difficulty { get; set; }
        public Domain.Enums.QuestionType Type { get; set; } = Domain.Enums.QuestionType.MultipleChoice;
        public decimal Points { get; set; } = 1m;
        public List<QuizOptionInlineDTO> Options { get; set; } = new();
    }
}
