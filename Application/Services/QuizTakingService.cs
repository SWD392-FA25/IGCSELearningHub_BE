using Application.Services.Interfaces;
using Application.Utils.Interfaces;
using Application.Wrappers;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.DTOs.QuizTaking;

namespace Application.Services
{
    public class QuizTakingService : IQuizTakingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<QuizTakingService> _logger;

        public QuizTakingService(IUnitOfWork uow, IDateTimeProvider clock, ILogger<QuizTakingService> logger)
        {
            _uow = uow;
            _clock = clock;
            _logger = logger;
        }

        public async Task<Result<QuizForTakeDTO>> GetQuizForTakeAsync(int quizId, int accountId, bool shuffleQuestions = false, bool shuffleOptions = false)
        {
            var quiz = await _uow.QuizRepository.GetAllQueryable(
                $"{nameof(Quiz.QuizQuestions)},{nameof(Quiz.QuizQuestions)}.{nameof(QuizQuestion.Question)},{nameof(Quiz.QuizQuestions)}.{nameof(QuizQuestion.Question)}.{nameof(Question.QuestionOptions)}")
                .FirstOrDefaultAsync(x => x.Id == quizId);
            if (quiz == null) return Result<QuizForTakeDTO>.Fail("Quiz not found.", 404);

            var qqs = quiz.QuizQuestions.AsEnumerable();
            if (shuffleQuestions) qqs = qqs.OrderBy(_ => Guid.NewGuid());
            else qqs = qqs.OrderBy(q => q.OrderIndex ?? int.MaxValue);

            var dto = new QuizForTakeDTO
            {
                QuizId = quiz.Id,
                Title = quiz.Title,
                TotalQuestions = quiz.TotalQuestions ?? quiz.QuizQuestions.Count,
                Questions = qqs.Select(qq =>
                {
                    var opts = qq.Question.QuestionOptions.AsEnumerable();
                    if (shuffleOptions) opts = opts.OrderBy(_ => Guid.NewGuid());
                    return new TakeQuestionDTO
                    {
                        QuestionId = qq.QuestionId,
                        OrderIndex = qq.OrderIndex,
                        Points = qq.Points,
                        Stem = qq.Question.Stem,
                        Options = opts.Select(op => new TakeOptionDTO
                        {
                            OptionId = op.Id,
                            Text = op.OptionText
                        }).ToList()
                    };
                }).ToList()
            };

            return Result<QuizForTakeDTO>.Success(dto);
        }

        public async Task<Result<int>> StartAttemptAsync(int quizId, int accountId)
        {
            var quiz = await _uow.QuizRepository.GetByIdAsync(quizId);
            if (quiz == null) return Result<int>.Fail("Quiz not found.", 404);

            var attempt = new QuizAttempt
            {
                QuizId = quizId,
                AccountId = accountId,
                AttemptDate = _clock.UtcNow
            };
            await _uow.QuizAttemptRepository.AddAsync(attempt);
            await _uow.SaveChangesAsync();

            return Result<int>.Success(attempt.Id, "Attempt started.", 201);
        }

        public async Task<Result<AttemptResultDTO>> SubmitAttemptAsync(int quizId, int attemptId, int accountId, SubmitAttemptDTO dto)
        {
            var attempt = await _uow.QuizAttemptRepository.GetAllQueryable(
                    $"{nameof(QuizAttempt.AttemptAnswers)},{nameof(QuizAttempt.Quiz)}," +
                    $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}," +
                    $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}.{nameof(QuizQuestion.Question)}," +
                    $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}.{nameof(QuizQuestion.Question)}.{nameof(Question.QuestionOptions)}")
                .FirstOrDefaultAsync(a => a.Id == attemptId && a.QuizId == quizId && a.AccountId == accountId);

            if (attempt == null) return Result<AttemptResultDTO>.Fail("Attempt not found.", 404);

            if (attempt.AttemptAnswers != null && attempt.AttemptAnswers.Any())
                return await BuildAttemptResult(attempt, 200);

            if (dto.Answers == null) dto.Answers = new();

            var mapQQ = attempt.Quiz.QuizQuestions.ToDictionary(k => k.QuestionId, v => (points: v.Points, question: v.Question));
            decimal total = attempt.Quiz.QuizQuestions.Sum(x => x.Points);
            decimal score = 0;

            using var tx = await _uow.BeginTransactionAsync();
            try
            {
                foreach (var item in dto.Answers)
                {
                    if (!mapQQ.TryGetValue(item.QuestionId, out var entry))
                        continue;

                    var q = entry.question;
                    var selected = q.QuestionOptions.FirstOrDefault(o => o.Id == item.SelectedOptionId);
                    if (selected == null) continue;

                    var isCorrect = selected.IsCorrect;
                    var awarded = isCorrect ? entry.points : 0m;
                    score += awarded;

                    await _uow.AttemptAnswerRepository.AddAsync(new AttemptAnswer
                    {
                        AttemptId = attempt.Id,
                        QuestionId = item.QuestionId,
                        SelectedOptionId = item.SelectedOptionId,
                        IsCorrect = isCorrect,
                        PointsAwarded = awarded
                    });
                }

                attempt.Score = score;
                _uow.QuizAttemptRepository.Update(attempt);
                await _uow.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Submit attempt failed");
                return Result<AttemptResultDTO>.Fail("Submit attempt failed.", 500).AddError("exception", ex.Message);
            }

            return await BuildAttemptResult(attempt, 200);
        }

        public async Task<Result<AttemptResultDTO>> GetAttemptDetailAsync(int quizId, int attemptId, int accountId)
        {
            var attempt = await _uow.QuizAttemptRepository.GetAllQueryable(
                    $"{nameof(QuizAttempt.AttemptAnswers)}," +
                    $"{nameof(QuizAttempt.Quiz)}," +
                    $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}," +
                    $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}.{nameof(QuizQuestion.Question)}," +
                    $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}.{nameof(QuizQuestion.Question)}.{nameof(Question.QuestionOptions)}")
                .FirstOrDefaultAsync(a => a.Id == attemptId && a.QuizId == quizId && a.AccountId == accountId);

            if (attempt == null) return Result<AttemptResultDTO>.Fail("Attempt not found.", 404);

            return await BuildAttemptResult(attempt, 200);
        }

        public async Task<PagedResult<AttemptSummaryDTO>> GetMyAttemptsAsync(int accountId, int? courseId, int? quizId, int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is <= 0 or > 100 ? 20 : pageSize;

            var query = _uow.QuizAttemptRepository.GetAllQueryable($"{nameof(QuizAttempt.Quiz)}")
                .Where(x => x.AccountId == accountId);

            if (quizId.HasValue) query = query.Where(x => x.QuizId == quizId.Value);
            if (courseId.HasValue) query = query.Where(x => x.Quiz.CourseId == courseId.Value);

            query = query.OrderByDescending(x => x.AttemptDate);
            return await query.ToPagedResultAsync(page, pageSize, x => new AttemptSummaryDTO
            {
                AttemptId = x.Id,
                QuizId = x.QuizId,
                QuizTitle = x.Quiz.Title,
                Score = x.Score,
                MaxScore = x.Quiz.QuizQuestions.Sum(q => q.Points),
                AttemptDate = x.AttemptDate
            });
        }

        private async Task<Result<AttemptResultDTO>> BuildAttemptResult(QuizAttempt attempt, int statusCode)
        {
            if (attempt.Quiz == null)
            {
                attempt = await _uow.QuizAttemptRepository.GetAllQueryable(
                        $"{nameof(QuizAttempt.AttemptAnswers)}," +
                        $"{nameof(QuizAttempt.Quiz)}," +
                        $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}," +
                        $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}.{nameof(QuizQuestion.Question)}," +
                        $"{nameof(QuizAttempt.Quiz)}.{nameof(Quiz.QuizQuestions)}.{nameof(QuizQuestion.Question)}.{nameof(Question.QuestionOptions)}")
                    .FirstAsync(x => x.Id == attempt.Id);
            }

            var mapAnswers = attempt.AttemptAnswers?.ToDictionary(k => k.QuestionId, v => v) ?? new();
            decimal max = attempt.Quiz.QuizQuestions.Sum(q => q.Points);
            decimal score = attempt.Score ?? mapAnswers.Values.Sum(a => a.PointsAwarded ?? 0);

            var details = attempt.Quiz.QuizQuestions
                .OrderBy(q => q.OrderIndex)
                .Select(qq =>
                {
                    mapAnswers.TryGetValue(qq.QuestionId, out var aa);
                    var isCorrect = aa?.IsCorrect ?? false;
                    return new AttemptResultDetail
                    {
                        QuestionId = qq.QuestionId,
                        SelectedOptionId = aa?.SelectedOptionId,
                        IsCorrect = isCorrect,
                        Awarded = aa?.PointsAwarded ?? 0,
                        Explanation = qq.Question.Explanation
                    };
                }).ToList();

            var result = new AttemptResultDTO
            {
                AttemptId = attempt.Id,
                QuizId = attempt.QuizId,
                Score = score,
                MaxScore = max,
                AttemptDate = attempt.AttemptDate,
                Details = details
            };

            return Result<AttemptResultDTO>.Success(result).SetStatusCode(statusCode);
        }
    }
}
