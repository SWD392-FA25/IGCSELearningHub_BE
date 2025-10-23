using Application.Services.Interfaces;
using Application.ViewModels.Packages;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/packages")]
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminPackagesController : ControllerBase
    {
        private readonly ICoursePackageAdminService _service;
        public AdminPackagesController(ICoursePackageAdminService service) => _service = service;

        // GET /admin/packages?q=&sort=&pageNumber=&pageSize=
        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] string? q,
            [FromQuery] string? sort,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetListAsync(q, sort, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        // GET /admin/packages/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST /admin/packages
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PackageCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        // PUT /admin/packages/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PackageUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE /admin/packages/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST /admin/packages/{id}/courses  (thêm nhiều khóa vào gói)
        [HttpPost("{id:int}/courses")]
        public async Task<IActionResult> AddCourses([FromRoute] int id, [FromBody] PackageAddCoursesDTO dto)
        {
            var result = await _service.AddCoursesAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE /admin/packages/{id}/courses/{courseId} (gỡ 1 khóa ra khỏi gói)
        [HttpDelete("{id:int}/courses/{courseId:int}")]
        public async Task<IActionResult> RemoveCourse([FromRoute] int id, [FromRoute] int courseId)
        {
            var result = await _service.RemoveCourseAsync(id, courseId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
