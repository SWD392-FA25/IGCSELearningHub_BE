using Application.Services.Interfaces;
using Application.ViewModels.QuizTaking;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/student/quizzes")]
    //[Authorize]
    public class StudentQuizzesController : ControllerBase
    {
        private readonly IQuizTakingService _service;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public StudentQuizzesController(IQuizTakingService service) => _service = service;

        // GET /student/quizzes/{quizId}/for-take?shuffleQuestions=true&shuffleOptions=true
        [HttpGet("{quizId:int}/for-take")]
        public async Task<IActionResult> GetForTake([FromRoute] int quizId, [FromQuery] bool shuffleQuestions = false, [FromQuery] bool shuffleOptions = false)
        {
            var result = await _service.GetQuizForTakeAsync(quizId, CurrentUserId, shuffleQuestions, shuffleOptions);
            return StatusCode(result.StatusCode, result);
        }

        // POST /student/quizzes/{quizId}/attempts
        [HttpPost("{quizId:int}/attempts")]
        public async Task<IActionResult> StartAttempt([FromRoute] int quizId)
        {
            var result = await _service.StartAttemptAsync(quizId, CurrentUserId);
            return StatusCode(result.StatusCode, result);
        }

        // POST /student/quizzes/{quizId}/attempts/{attemptId}/submit
        [HttpPost("{quizId:int}/attempts/{attemptId:int}/submit")]
        public async Task<IActionResult> Submit([FromRoute] int quizId, [FromRoute] int attemptId, [FromBody] SubmitAttemptDTO dto)
        {
            var result = await _service.SubmitAttemptAsync(quizId, attemptId, CurrentUserId, dto);
            return StatusCode(result.StatusCode, result);
        }

        // GET /student/quizzes/{quizId}/attempts/{attemptId}
        [HttpGet("{quizId:int}/attempts/{attemptId:int}")]
        public async Task<IActionResult> AttemptDetail([FromRoute] int quizId, [FromRoute] int attemptId)
        {
            var result = await _service.GetAttemptDetailAsync(quizId, attemptId, CurrentUserId);
            return StatusCode(result.StatusCode, result);
        }

        // GET /student/quiz-attempts?courseId=&quizId=&pageNumber=&pageSize=
        [HttpGet("~/api/v{version:apiVersion}/student/quiz-attempts")]
        public async Task<IActionResult> MyAttempts([FromQuery] int? courseId, [FromQuery] int? quizId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetMyAttemptsAsync(CurrentUserId, courseId, quizId, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }
    }
}
