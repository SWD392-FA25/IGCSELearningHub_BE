using Application.DTOs.Assignments;
using Domain.Entities;
using System.Linq.Expressions;
using System.Linq;

namespace Application.Mapping
{
    public static class AssignmentProjections
    {
        public static readonly Expression<Func<Assignment, AssignmentAdminListItemDTO>> AdminListItem =
            assignment => new AssignmentAdminListItemDTO
            {
                Id = assignment.Id,
                CourseId = assignment.CourseId,
                Title = assignment.Title,
                CreatedAt = assignment.CreatedAt,
                SubmissionCount = assignment.Submissions.Count(s => !s.IsDeleted)
            };

        public static readonly Expression<Func<Submission, SubmissionListItemDTO>> SubmissionListItem =
            submission => new SubmissionListItemDTO
            {
                SubmissionId = submission.Id,
                AccountId = submission.AccountId,
                AccountUserName = submission.Account.UserName,
                Score = submission.Score,
                SubmittedDate = submission.SubmittedDate
            };
    }
}
