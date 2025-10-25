using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/me/enrollments")]
    [Authorize(Roles = "Student")]
    public class MyEnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentStudentService _service;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public MyEnrollmentsController(IEnrollmentStudentService service) => _service = service;

        // GET /me/enrollments?pageNumber=&pageSize=
        [HttpGet]
        public async Task<IActionResult> GetMine([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetMyEnrollmentsAsync(CurrentUserId, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        // GET /me/enrollments/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMyDetail([FromRoute] int id)
        {
            var result = await _service.GetMyEnrollmentDetailAsync(CurrentUserId, id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
