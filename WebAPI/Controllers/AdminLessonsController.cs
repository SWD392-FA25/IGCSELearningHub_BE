using Application.DTOs.Lessons;
using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminLessonsController : ControllerBase
    {
        private readonly ILessonAdminService _service;
        public AdminLessonsController(ILessonAdminService service) => _service = service;

        // POST /admin/courses/{courseId}/lessons
        [HttpPost]
        [Route("api/v{version:apiVersion}/admin/courses/{courseId:int}/lessons")]
        public async Task<IActionResult> Create([FromRoute] int courseId, [FromBody] LessonCreateDTO dto)
        {
            var result = await _service.CreateAsync(courseId, dto);
            return StatusCode(result.StatusCode, result);
        }

        // PUT /admin/lessons/{lessonId}
        [HttpPut]
        [Route("api/v{version:apiVersion}/admin/lessons/{lessonId:int}")]
        public async Task<IActionResult> Update([FromRoute] int lessonId, [FromBody] LessonUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(lessonId, dto);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE /admin/lessons/{lessonId}
        [HttpDelete]
        [Route("api/v{version:apiVersion}/admin/lessons/{lessonId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int lessonId)
        {
            var result = await _service.DeleteAsync(lessonId);
            return StatusCode(result.StatusCode, result);
        }

        // PATCH /admin/lessons/{lessonId}/order
        [HttpPatch]
        [Route("api/v{version:apiVersion}/admin/lessons/{lessonId:int}/order")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int lessonId, [FromBody] LessonOrderUpdateDTO dto)
        {
            var result = await _service.UpdateOrderAsync(lessonId, dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}

