using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Quiz
{
    public class QuizSummaryDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public int TotalQuestions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
