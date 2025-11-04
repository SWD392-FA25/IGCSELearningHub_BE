using System;

namespace Application.DTOs.Assignments
{
    public class StudentAssignmentItemDTO
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Submitted { get; set; }
    }
}
