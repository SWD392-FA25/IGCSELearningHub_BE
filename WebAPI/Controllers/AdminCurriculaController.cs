using Application.DTOs.Curricula;
using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/curriculums")]
    //[Authorize(Roles = "Admin,Teacher")]
    public class AdminCurriculaController : ControllerBase
    {
        private readonly ICurriculumService _service;
        public AdminCurriculaController(ICurriculumService service) => _service = service;

        [HttpGet("{curriculumId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int curriculumId)
        {
            var result = await _service.GetByIdAsync(curriculumId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("~/api/v{version:apiVersion}/courses/{courseId:int}/curriculums")]
        public async Task<IActionResult> GetByCourse([FromRoute] int courseId)
        {
            var result = await _service.GetByCourseAsync(courseId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("~/api/v{version:apiVersion}/courses/{courseId:int}/curriculums")]
        public async Task<IActionResult> Create([FromRoute] int courseId, [FromBody] CurriculumCreateDTO dto)
        {
            var result = await _service.CreateAsync(courseId, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{curriculumId:int}")]
        public async Task<IActionResult> Update([FromRoute] int curriculumId, [FromBody] CurriculumUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(curriculumId, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{curriculumId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int curriculumId)
        {
            var result = await _service.DeleteAsync(curriculumId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
