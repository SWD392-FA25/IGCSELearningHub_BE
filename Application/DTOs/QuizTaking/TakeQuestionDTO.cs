using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.QuizTaking
{
    public class TakeQuestionDTO
    {
        public int QuestionId { get; set; }
        public int? OrderIndex { get; set; }
        public decimal Points { get; set; }
        public string Stem { get; set; } = null!;
        public List<TakeOptionDTO> Options { get; set; } = new();
    }
}
