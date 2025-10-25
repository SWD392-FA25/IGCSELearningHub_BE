using Application.DTOs.Submissions;
using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/me/assignments")]
    [Authorize(Roles = "Student,Admin,Teacher")]
    public class MyAssignmentsController : ControllerBase
    {
        private readonly IStudentSubmissionService _svc;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public MyAssignmentsController(IStudentSubmissionService svc) => _svc = svc;

        [HttpPost("{assignmentId:int}/submissions")]
        public async Task<IActionResult> Submit([FromRoute] int assignmentId, [FromBody] CreateSubmissionRequest req)
        {
            var result = await _svc.SubmitAsync(CurrentUserId, assignmentId, req);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{assignmentId:int}/submissions")]
        public async Task<IActionResult> MySubmissions([FromRoute] int assignmentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _svc.GetMySubmissionsAsync(CurrentUserId, assignmentId, page, pageSize);
            return StatusCode(result.StatusCode, result);
        }
    }
}
