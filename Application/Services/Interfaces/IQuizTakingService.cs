using Application.ViewModels.QuizTaking;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IQuizTakingService
    {
        Task<Result<QuizForTakeDTO>> GetQuizForTakeAsync(int quizId, int accountId, bool shuffleQuestions = false, bool shuffleOptions = false);
        Task<Result<int>> StartAttemptAsync(int quizId, int accountId); // trả AttemptId
        Task<Result<AttemptResultDTO>> SubmitAttemptAsync(int quizId, int attemptId, int accountId, SubmitAttemptDTO dto);
        Task<Result<AttemptResultDTO>> GetAttemptDetailAsync(int quizId, int attemptId, int accountId);
        Task<PagedResult<AttemptSummaryDTO>> GetMyAttemptsAsync(int accountId, int? courseId, int? quizId, int page, int pageSize);
    }
}
