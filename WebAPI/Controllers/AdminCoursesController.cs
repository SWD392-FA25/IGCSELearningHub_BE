using Application.DTOs.Courses;
using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/courses")]
    //[Authorize]
    public class AdminCoursesController : ControllerBase
    {
        private readonly ICourseAdminService _service;

        public AdminCoursesController(ICourseAdminService service) => _service = service;

        // GET /admin/courses?q=&level=&sort=&pageNumber=&pageSize=
        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] string? q,
            [FromQuery] string? level,
            [FromQuery] string? sort,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetListAsync(q, level, pageNumber, pageSize, sort);
            return StatusCode(result.StatusCode, result);
        }

        // GET /admin/courses/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST /admin/courses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        // PUT /admin/courses/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CourseUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE /admin/courses/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
