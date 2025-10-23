using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Quiz
{
    public class QuizDetailOptionDTO
    {
        public int OptionId { get; set; }
        public string Text { get; set; } = null!;
        public bool IsCorrect { get; set; }   // CHỈ hiển thị ở API quản trị
    }
}
