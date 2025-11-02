using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/packages")]
    [AllowAnonymous]
    public class PackagesController : ControllerBase
    {
        private readonly ICoursePackagePublicService _service;
        public PackagesController(ICoursePackagePublicService service) => _service = service;

        // GET /packages?q=&sort=&pageNumber=&pageSize=
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

        // GET /packages/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}

