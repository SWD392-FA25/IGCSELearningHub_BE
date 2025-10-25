using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Enrollments
{
    public class EnrollmentAdminListItemDTO
    {
        public int EnrollmentId { get; set; }
        public int AccountId { get; set; }
        public string AccountUserName { get; set; } = null!;
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
