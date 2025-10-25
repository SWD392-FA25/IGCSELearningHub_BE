using Application.Services.Interfaces;
using Application.ViewModels.Enrollments;
using Application.Wrappers;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EnrollmentStudentService : IEnrollmentStudentService
    {
        private readonly IUnitOfWork _uow;

        public EnrollmentStudentService(IUnitOfWork uow) => _uow = uow;

        public async Task<PagedResult<MyEnrollmentItemDTO>> GetMyEnrollmentsAsync(int accountId, int page, int pageSize)
        {
            var query = _uow.EnrollmentRepository
                .GetAllQueryable($"{nameof(Enrollment.Course)},{nameof(Enrollment.Progresses)}")
                .Where(e => e.AccountId == accountId);
            query = query.OrderByDescending(e => e.EnrollmentDate);
            return await query.ToPagedResultAsync(page, pageSize, e => new MyEnrollmentItemDTO
            {
                EnrollmentId = e.Id,
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                CompletedPercent = e.Progresses.OrderByDescending(p => p.ModifiedAt)
                    .Select(p => (byte?)p.CompletedPercent).FirstOrDefault()
            });
        }

        public async Task<Result<MyEnrollmentDetailDTO>> GetMyEnrollmentDetailAsync(int accountId, int enrollmentId)
        {
            var e = await _uow.EnrollmentRepository.GetAllQueryable(
                    $"{nameof(Enrollment.Course)},{nameof(Enrollment.Progresses)}")
                .FirstOrDefaultAsync(x => x.Id == enrollmentId && x.AccountId == accountId);

            if (e == null) return Result<MyEnrollmentDetailDTO>.Fail("Enrollment not found.", 404);

            var dto = new MyEnrollmentDetailDTO
            {
                EnrollmentId = e.Id,
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title,
                CourseLevel = e.Course.Level,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                CompletedPercent = e.Progresses.OrderByDescending(p => p.ModifiedAt).Select(p => (byte?)p.CompletedPercent).FirstOrDefault(),
                LastAccessDate = e.Progresses.OrderByDescending(p => p.LastAccessDate).Select(p => p.LastAccessDate).FirstOrDefault()
            };

            return Result<MyEnrollmentDetailDTO>.Success(dto);
        }
    }
}
