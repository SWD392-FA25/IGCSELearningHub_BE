using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/me/courses/{courseId:int}/lessons")]
    [Authorize(Roles = "Student,Admin,Teacher")]
    public class MyCourseLessonsController : ControllerBase
    {
        private readonly ILessonStudentService _service;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public MyCourseLessonsController(ILessonStudentService service)
        {
            _service = service;
        }

        // GET /me/courses/{courseId}/lessons
        [HttpGet]
        public async Task<IActionResult> GetMine([FromRoute] int courseId)
        {
            var result = await _service.GetMyLessonsAsync(CurrentUserId, courseId);
            return StatusCode(result.StatusCode, result);
        }

        // GET /me/courses/{courseId}/lessons/{lessonId}
        [HttpGet("{lessonId:int}")]
        public async Task<IActionResult> GetMyLesson([FromRoute] int courseId, [FromRoute] int lessonId)
        {
            var result = await _service.GetMyLessonDetailAsync(CurrentUserId, courseId, lessonId);
            return StatusCode(result.StatusCode, result);
        }

        // PATCH /me/courses/{courseId}/lessons/{lessonId}/complete
        [HttpPatch("{lessonId:int}/complete")]
        public async Task<IActionResult> CompleteLesson([FromRoute] int courseId, [FromRoute] int lessonId)
        {
            var result = await _service.CompleteLessonAsync(CurrentUserId, courseId, lessonId);
            return StatusCode(result.StatusCode, result);
        }
    }
}

