using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/student/assignments")]
    [Authorize]
    public class StudentAssignmentsController : ControllerBase
    {
        private readonly IAssignmentStudentService _service;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public StudentAssignmentsController(IAssignmentStudentService service)
        {
            _service = service;
        }

        // GET /student/assignments?courseId=&submitted=&pageNumber=&pageSize=&sort=
        [HttpGet]
        public async Task<IActionResult> GetByCourse(
            [FromQuery] int courseId,
            [FromQuery] bool? submitted,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sort = null)
        {
            if (courseId <= 0) return BadRequest("courseId is required");
            var result = await _service.GetListByCourseAsync(CurrentUserId, courseId, submitted, pageNumber, pageSize, sort);
            return StatusCode(result.StatusCode, result);
        }
    }
}
