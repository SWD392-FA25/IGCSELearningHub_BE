using Application.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/courses")]
    [AllowAnonymous]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseCatalogService _service;

        public CoursesController(ICourseCatalogService service) => _service = service;

        // GET /courses?q=&level=&priceMin=&priceMax=&sort=&pageNumber=&pageSize=
        [HttpGet]
        public async Task<IActionResult> GetCatalog(
            [FromQuery] string? q,
            [FromQuery] string? level,
            [FromQuery] decimal? priceMin,
            [FromQuery] decimal? priceMax,
            [FromQuery] string? sort,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetCatalogAsync(q, level, priceMin, priceMax, pageNumber, pageSize, sort);
            return StatusCode(result.StatusCode, result);
        }

        // GET /courses/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
