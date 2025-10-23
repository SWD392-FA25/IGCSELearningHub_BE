using Application.Services.Interfaces;
using Application.ViewModels.Livestreams;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/livestreams")]
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminLivestreamsController : ControllerBase
    {
        private readonly ILivestreamAdminService _service;

        public AdminLivestreamsController(ILivestreamAdminService service) => _service = service;

        // GET /admin/livestreams?courseId=&q=&from=&to=&sort=&pageNumber=&pageSize=
        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] int? courseId,
            [FromQuery] string? q,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? sort,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetListAsync(courseId, q, from, to, sort, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        // GET /admin/livestreams/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST /admin/livestreams
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LivestreamCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        // PUT /admin/livestreams/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] LivestreamUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE /admin/livestreams/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // GET /admin/livestreams/{livestreamId}/registrations?paymentStatus=&pageNumber=&pageSize=
        [HttpGet("{livestreamId:int}/registrations")]
        public async Task<IActionResult> GetRegistrations(
            [FromRoute] int livestreamId,
            [FromQuery] string? paymentStatus,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetRegistrationsAsync(livestreamId, paymentStatus, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        // PATCH /admin/livestreams/registrations/{registrationId}/payment-status
        [HttpPatch("registrations/{registrationId:int}/payment-status")]
        public async Task<IActionResult> UpdateRegistrationPaymentStatus(
            [FromRoute] int registrationId,
            [FromBody] UpdateRegistrationPaymentStatusDTO dto)
        {
            var result = await _service.UpdateRegistrationPaymentStatusAsync(registrationId, dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
