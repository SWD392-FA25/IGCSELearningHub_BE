using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Quiz
{
    public class QuizOptionInlineDTO
    {
        public string Text { get; set; } = null!;
        public bool IsCorrect { get; set; }
    }
}
