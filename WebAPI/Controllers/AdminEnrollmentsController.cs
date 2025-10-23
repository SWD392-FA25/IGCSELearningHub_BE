using Application.Services.Interfaces;
using Application.ViewModels.Enrollments;
using Asp.Versioning;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/enrollments")]
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminEnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentAdminService _service;
        public AdminEnrollmentsController(IEnrollmentAdminService service) => _service = service;

        // GET /admin/enrollments?accountId=&courseId=&status=&from=&to=&pageNumber=&pageSize=&sort=
        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] int? accountId,
            [FromQuery] int? courseId,
            [FromQuery] EnrollmentStatus? status,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sort = null)
        {
            var result = await _service.GetListAsync(accountId, courseId, status, from, to, pageNumber, pageSize, sort);
            return StatusCode(result.StatusCode, result);
        }

        // GET /admin/enrollments/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST /admin/enrollments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        // PATCH /admin/enrollments/{id}/status
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromBody] EnrollmentUpdateStatusDTO dto)
        {
            var result = await _service.UpdateStatusAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE /admin/enrollments/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST /admin/enrollments/create-from-order
        [HttpPost("create-from-order")]
        public async Task<IActionResult> CreateFromOrder([FromBody] CreateEnrollmentFromOrderDTO dto)
        {
            var result = await _service.CreateFromOrderAsync(dto.OrderId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
