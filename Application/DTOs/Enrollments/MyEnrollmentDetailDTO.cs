using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Enrollments
{
    public class MyEnrollmentDetailDTO
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public string? CourseLevel { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
        public byte? CompletedPercent { get; set; }
        public DateTime? LastAccessDate { get; set; }
    }
}
