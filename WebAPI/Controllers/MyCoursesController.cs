using System.Threading.Tasks;
using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/me/courses")]
    [Authorize(Roles = "Student,Admin,Teacher")]
    public class MyCoursesController : ControllerBase
    {
        private readonly ILessonStudentService _lessonStudentService;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public MyCoursesController(ILessonStudentService lessonStudentService)
        {
            _lessonStudentService = lessonStudentService;
        }

        [HttpGet("{courseId:int}")]
        public async Task<IActionResult> GetCourseDetail([FromRoute] int courseId)
        {
            var result = await _lessonStudentService.GetMyCourseDetailAsync(CurrentUserId, courseId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
