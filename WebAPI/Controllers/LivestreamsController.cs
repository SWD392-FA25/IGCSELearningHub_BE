using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/livestreams")]
    [AllowAnonymous]
    public class LivestreamsController : ControllerBase
    {
        private readonly ILivestreamPublicService _service;
        public LivestreamsController(ILivestreamPublicService service) => _service = service;

        // GET /livestreams?courseId=&from=&to=&sort=&pageNumber=&pageSize=
        [HttpGet]
        public async Task<IActionResult> GetUpcoming(
            [FromQuery] int? courseId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? sort,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetUpcomingListAsync(courseId, from, to, sort, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        // GET /livestreams/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}

