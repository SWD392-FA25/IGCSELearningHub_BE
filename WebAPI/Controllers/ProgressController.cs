using Application.DTOs.Progress;
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
    [Route("api/v{version:apiVersion}")]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        // ===== Student =====

        [HttpGet("me/courses/{courseId:int}/progress")]
        [Authorize(Roles = "Student,Admin,Teacher")]
        public async Task<IActionResult> GetMyProgress(int courseId)
        {
            var result = await _progressService.GetMyProgressAsync(CurrentUserId, courseId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("me/courses/{courseId:int}/progress")]
        [Authorize(Roles = "Student,Admin,Teacher")]
        public async Task<IActionResult> UpdateMyProgress(int courseId, [FromBody] UpdateProgressRequest req)
        {
            var result = await _progressService.UpdateMyProgressAsync(CurrentUserId, courseId, req);
            return StatusCode(result.StatusCode, result);
        }

        // ===== Admin/Teacher =====
        [HttpGet("admin/courses/{courseId:int}/progress")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetCourseProgress(int courseId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _progressService.GetCourseProgressAsync(courseId, page, pageSize);
            return StatusCode(result.StatusCode, result);
        }
    }
}
