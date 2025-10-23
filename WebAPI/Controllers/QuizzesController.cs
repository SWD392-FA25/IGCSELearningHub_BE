using Application.Services.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Quiz;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _service;
        private readonly ILogger<QuizzesController> _logger;

        public QuizzesController(IQuizService service, ILogger<QuizzesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET /quizzes?courseId=&page=&pageSize=
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? courseId, [FromQuery] PaginationFilter paging)
        {
            var result = await _service.GetListAsync(courseId, paging.PageNumber, paging.PageSize);
            return StatusCode(result.StatusCode, result);
        }

        // GET /quizzes/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            var result = await _service.GetDetailAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST /quizzes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuizCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        // PUT /quizzes/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] QuizUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE /quizzes/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
