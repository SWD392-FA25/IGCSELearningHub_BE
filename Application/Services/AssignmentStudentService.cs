using Application.DTOs.Assignments;
using Application.Extensions;
using Application.Services.Interfaces;
using Application.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AssignmentStudentService : IAssignmentStudentService
    {
        private readonly IUnitOfWork _uow;
        public AssignmentStudentService(IUnitOfWork uow) => _uow = uow;

        public async Task<PagedResult<StudentAssignmentItemDTO>> GetListByCourseAsync(
            int accountId,
            int courseId,
            bool? submitted,
            int page,
            int pageSize,
            string? sort)
        {
            // ensure enrolled
            var enrolled = await _uow.EnrollmentRepository.GetAllQueryable()
                .AnyAsync(e => e.AccountId == accountId && e.CourseId == courseId && !e.IsDeleted);
            if (!enrolled)
                return PagedResult<StudentAssignmentItemDTO>.Success(new List<StudentAssignmentItemDTO>(), 0, page, pageSize)
                    .AddDetail("warning", "Enrollment required") as PagedResult<StudentAssignmentItemDTO>;

            var mySubmissions = _uow.SubmissionRepository.GetAllQueryable()
                .Where(s => s.AccountId == accountId)
                .Select(s => s.AssignmentId)
                .Distinct();

            var query = _uow.AssignmentRepository.GetAllQueryable()
                .Where(a => a.CourseId == courseId);

            if (submitted.HasValue)
            {
                if (submitted.Value)
                    query = query.Where(a => mySubmissions.Contains(a.Id));
                else
                    query = query.Where(a => !mySubmissions.Contains(a.Id));
            }

            // sort: createdAt_desc (default) | createdAt_asc | title_asc | title_desc | submitted_first
            query = (sort ?? string.Empty).ToLower() switch
            {
                "createdat_asc" => query.OrderBy(a => a.CreatedAt),
                "title_asc" => query.OrderBy(a => a.Title),
                "title_desc" => query.OrderByDescending(a => a.Title),
                "submitted_first" => query.OrderByDescending(a => mySubmissions.Contains(a.Id)).ThenByDescending(a => a.CreatedAt),
                _ => query.OrderByDescending(a => a.CreatedAt)
            };

            return await query.ToPagedResultAsync(page, pageSize, a => new StudentAssignmentItemDTO
            {
                AssignmentId = a.Id,
                CourseId = a.CourseId,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                Submitted = mySubmissions.Contains(a.Id)
            });
        }
    }
}
