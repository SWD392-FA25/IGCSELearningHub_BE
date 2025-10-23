using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Enrollments
{
    public class EnrollmentCreateDTO
    {
        public int AccountId { get; set; }
        public int CourseId { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
    }
}
