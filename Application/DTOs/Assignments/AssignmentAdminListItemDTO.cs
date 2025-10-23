using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Assignments
{
    public class AssignmentAdminListItemDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int SubmissionCount { get; set; }
    }
}
