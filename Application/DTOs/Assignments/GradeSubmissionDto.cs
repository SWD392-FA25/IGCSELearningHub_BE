using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Assignments
{
    public class GradeSubmissionDto
    {
        public decimal? Score { get; set; } // cho phép null (chưa chấm)
    }
}
