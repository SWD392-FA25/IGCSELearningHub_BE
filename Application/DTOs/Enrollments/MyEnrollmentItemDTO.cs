using Domain.Enums;

namespace Application.ViewModels.Enrollments
{
    public class MyEnrollmentItemDTO
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
        public byte? CompletedPercent { get; set; }
    }
}
