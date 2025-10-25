using Application.Services.Interfaces;
using Application.DTOs.Assignments;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin")]
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminAssignmentsController : ControllerBase
    {
        private readonly IAssignmentAdminService _service;

        public AdminAssignmentsController(IAssignmentAdminService service) => _service = service;

        // GET /admin/assignments?courseId=&q=&sort=&pageNumber=&pageSize=
        [HttpGet("assignments")]
        public async Task<IActionResult> GetAssignments(
            [FromQuery] int? courseId,
            [FromQuery] string? q,
            [FromQuery] string? sort,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetListAsync(courseId, q, pageNumber, pageSize, sort);
            return StatusCode(result.StatusCode, result);
        }

        // GET /admin/assignments/{id}
        [HttpGet("assignments/{id:int}")]
        public async Task<IActionResult> GetAssignmentDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST /admin/assignments
        [HttpPost("assignments")]
        public async Task<IActionResult> CreateAssignment([FromBody] AssignmentCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        // PUT /admin/assignments/{id}
        [HttpPut("assignments/{id:int}")]
        public async Task<IActionResult> UpdateAssignment([FromRoute] int id, [FromBody] AssignmentUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE /admin/assignments/{id}
        [HttpDelete("assignments/{id:int}")]
        public async Task<IActionResult> DeleteAssignment([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // ===== Submissions (view & grade) =====

        // GET /admin/assignments/{assignmentId}/submissions?pageNumber=&pageSize=
        [HttpGet("assignments/{assignmentId:int}/submissions")]
        public async Task<IActionResult> GetSubmissions([FromRoute] int assignmentId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetSubmissionsAsync(assignmentId, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        // GET /admin/submissions/{submissionId}
        [HttpGet("submissions/{submissionId:int}")]
        public async Task<IActionResult> GetSubmissionDetail([FromRoute] int submissionId)
        {
            var result = await _service.GetSubmissionDetailAsync(submissionId);
            return StatusCode(result.StatusCode, result);
        }

        // PATCH /admin/submissions/{submissionId}/grade
        [HttpPatch("submissions/{submissionId:int}/grade")]
        public async Task<IActionResult> GradeSubmission([FromRoute] int submissionId, [FromBody] GradeSubmissionDto dto)
        {
            var result = await _service.GradeSubmissionAsync(submissionId, dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}

