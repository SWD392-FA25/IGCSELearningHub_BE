using Application.Services.Interfaces;
using Application.Wrappers;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/courses/{courseId:int}/lessons")]
    [AllowAnonymous]
    public class CourseLessonsController : ControllerBase
    {
        private readonly ILessonPublicService _service;
        public CourseLessonsController(ILessonPublicService service) => _service = service;

        private int? CurrentUserId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(id, out var parsed) ? parsed : (int?)null;
        }

        // GET /courses/{courseId}/lessons
        [HttpGet]
        public async Task<IActionResult> GetList([FromRoute] int courseId)
        {
            var result = await _service.GetLessonsAsync(courseId, CurrentUserId());
            return StatusCode(result.StatusCode, result);
        }

        // GET /courses/{courseId}/lessons/{lessonId}
        [HttpGet("{lessonId:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int courseId, [FromRoute] int lessonId)
        {
            var result = await _service.GetLessonDetailAsync(courseId, lessonId, CurrentUserId());
            return StatusCode(result.StatusCode, result);
        }
    }
}

