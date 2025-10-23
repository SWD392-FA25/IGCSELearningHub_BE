using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.QuizTaking
{
    public class AttemptResultDetail
    {
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }
        public bool IsCorrect { get; set; }
        public decimal Awarded { get; set; }
        public string? Explanation { get; set; }
    }
}
