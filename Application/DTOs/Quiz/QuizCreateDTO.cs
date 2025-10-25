using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Quiz
{
    public class QuizCreateDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public int? TimeLimitSeconds { get; set; } 
        public bool RandomizeQuestions { get; set; } = true;
        public bool RandomizeOptions { get; set; } = true;
        public List<QuizQuestionInlineDTO> Questions { get; set; } = new();
    }
}
