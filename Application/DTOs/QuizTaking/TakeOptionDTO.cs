using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.QuizTaking
{
    public class TakeOptionDTO
    {
        public int OptionId { get; set; }
        public string Text { get; set; } = null!;
    }
}
