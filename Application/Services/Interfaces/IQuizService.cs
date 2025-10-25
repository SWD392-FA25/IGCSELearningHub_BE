using Application.DTOs.Quiz;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IQuizService
    {
        Task<PagedResult<QuizSummaryDTO>> GetListAsync(int? courseId, int page, int pageSize);
        Task<Result<QuizDetailDTO>> GetDetailAsync(int quizId);
        Task<Result<QuizDetailDTO>> CreateAsync(QuizCreateDTO dto);
        Task<Result<QuizDetailDTO>> UpdateAsync(int quizId, QuizUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int quizId);
    }
}
