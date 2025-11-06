using Application.DTOs.Lessons;
using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin")]
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminLessonsController : ControllerBase
    {
        private readonly ILessonAdminService _service;
        public AdminLessonsController(ILessonAdminService service) => _service = service;

        /// <summary>
        /// Create a new lesson under the specified course unit.
        /// </summary>
        [HttpPost("courses/{courseId:int}/lessons")]
        public async Task<IActionResult> Create([FromRoute] int courseId, [FromBody] LessonCreateDTO dto)
        {
            var result = await _service.CreateAsync(courseId, dto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update lesson details.
        /// </summary>
        [HttpPut("lessons/{lessonId:int}")]
        public async Task<IActionResult> Update([FromRoute] int lessonId, [FromBody] LessonUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(lessonId, dto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Soft delete a lesson.
        /// </summary>
        [HttpDelete("lessons/{lessonId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int lessonId)
        {
            var result = await _service.DeleteAsync(lessonId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update the display order of a lesson within its unit.
        /// </summary>
        [HttpPatch("lessons/{lessonId:int}/order")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int lessonId, [FromBody] LessonOrderUpdateDTO dto)
        {
            var result = await _service.UpdateOrderAsync(lessonId, dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
