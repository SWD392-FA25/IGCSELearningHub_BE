using Application.Services.Interfaces;
using Application.ViewModels.Quiz;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<QuizService> _logger;
        private readonly IMapper _mapper;

        public QuizService(IUnitOfWork uow, ILogger<QuizService> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagedResult<QuizSummaryDTO>> GetListAsync(int? courseId, int page, int pageSize)
        {
            var query = _uow.QuizRepository.GetAllQueryable();
            if (courseId.HasValue) query = query.Where(x => x.CourseId == courseId.Value);

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new QuizSummaryDTO 
                {
                    Id = x.Id,
                    CourseId = x.CourseId,
                    Title = x.Title,
                    TotalQuestions = x.TotalQuestions ?? 0,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return PagedResult<QuizSummaryDTO>.Success(items, total, page, pageSize);
        }

        public async Task<Result<QuizDetailDTO>> GetDetailAsync(int quizId)
        {
            var quiz = await _uow.QuizRepository.GetAllQueryable(nameof(Quiz.QuizQuestions) + "," +
                                                                 nameof(Quiz.QuizQuestions) + "." + nameof(QuizQuestion.Question) + "," +
                                                                 nameof(Quiz.QuizQuestions) + "." + nameof(QuizQuestion.Question) + "." + nameof(Question.QuestionOptions))
                .FirstOrDefaultAsync(x => x.Id == quizId);
            if (quiz == null) return Result<QuizDetailDTO>.Fail("Quiz not found.", 404);

            var dto = new QuizDetailDTO
            {
                Id = quiz.Id,
                CourseId = quiz.CourseId,
                Title = quiz.Title,
                TotalQuestions = quiz.TotalQuestions ?? 0,
                CreatedAt = quiz.CreatedAt,
                RandomizeQuestions = false, // nếu bạn có field thì map vào
                RandomizeOptions = false,
                Questions = quiz.QuizQuestions.OrderBy(q => q.OrderIndex).Select(qq => new QuizDetailQuestionDTO
                {
                    QuestionId = qq.QuestionId,
                    OrderIndex = qq.OrderIndex,
                    Points = qq.Points,
                    Stem = qq.Question.Stem,
                    Explanation = qq.Question.Explanation,
                    Difficulty = qq.Question.Difficulty,
                    Type = qq.Question.Type,
                    Options = qq.Question.QuestionOptions.Select(op => new QuizDetailOptionDTO
                    {
                        OptionId = op.Id,
                        Text = op.OptionText,
                        IsCorrect = op.IsCorrect
                    }).ToList()
                }).ToList()
            };

            return Result<QuizDetailDTO>.Success(dto, statusCode: 200);
        }

        public async Task<Result<QuizDetailDTO>> CreateAsync(QuizCreateDTO dto)
        {
            // validate đơn giản
            if (dto.Questions == null || dto.Questions.Count == 0)
                return Result<QuizDetailDTO>.Fail("Quiz must have at least 1 question.", 400);

            foreach (var q in dto.Questions)
            {
                if (q.Options == null || q.Options.Count == 0)
                    return Result<QuizDetailDTO>.Fail("Each question must have options.", 400);
                if (!q.Options.Any(o => o.IsCorrect))
                    return Result<QuizDetailDTO>.Fail("Each question must have at least one correct option.", 400);
            }

            using var tx = await _uow.BeginTransactionAsync();
            try
            {
                var quiz = new Quiz
                {
                    CourseId = dto.CourseId,
                    Title = dto.Title,
                    TotalQuestions = dto.Questions.Count
                };
                await _uow.QuizRepository.AddAsync(quiz);
                await _uow.SaveChangesAsync();

                int order = 1;
                foreach (var qDto in dto.Questions)
                {
                    var question = new Question
                    {
                        CourseId = dto.CourseId,
                        Type = qDto.Type,
                        Stem = qDto.Stem,
                        Explanation = qDto.Explanation,
                        Difficulty = qDto.Difficulty
                    };
                    await _uow.QuestionRepository.AddAsync(question);
                    await _uow.SaveChangesAsync();

                    foreach (var op in qDto.Options)
                    {
                        await _uow.QuestionOptionRepository.AddAsync(new QuestionOption
                        {
                            QuestionId = question.Id,
                            OptionText = op.Text,
                            IsCorrect = op.IsCorrect
                        });
                    }
                    await _uow.SaveChangesAsync();

                    await _uow.QuizQuestionRepository.AddAsync(new QuizQuestion
                    {
                        QuizId = quiz.Id,
                        QuestionId = question.Id,
                        Points = qDto.Points <= 0 ? 1 : qDto.Points,
                        OrderIndex = order++
                    });
                    await _uow.SaveChangesAsync();
                }

                await tx.CommitAsync();
                return await GetDetailOk(quiz.Id, 201);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Create quiz failed");
                return Result<QuizDetailDTO>.Fail("Create quiz failed.", 500)
                    .AddError("exception", ex.Message);
            }
        }

        public async Task<Result<QuizDetailDTO>> UpdateAsync(int quizId, QuizUpdateDTO dto)
        {
            var quiz = await _uow.QuizRepository.GetByIdAsync(quizId);
            if (quiz == null) return Result<QuizDetailDTO>.Fail("Quiz not found.", 404);

            if (dto.Questions == null || dto.Questions.Count == 0)
                return Result<QuizDetailDTO>.Fail("Quiz must have at least 1 question.", 400);

            using var tx = await _uow.BeginTransactionAsync();
            try
            {
                // 1) Xóa (soft) toàn bộ mapping & câu hỏi cũ (vì không dùng question bank chia sẻ)
                var oldMappings = _uow.QuizQuestionRepository
                    .GetAllQueryable(nameof(QuizQuestion.Question) + "," + nameof(QuizQuestion.Question) + "." + nameof(Question.QuestionOptions))
                    .Where(x => x.QuizId == quizId)
                    .ToList();

                foreach (var map in oldMappings)
                {
                    // soft delete options
                    foreach (var op in map.Question.QuestionOptions) _uow.QuestionOptionRepository.SoftDelete(op);
                    // soft delete question
                    _uow.QuestionRepository.SoftDelete(map.Question);
                    // soft delete mapping
                    _uow.QuizQuestionRepository.SoftDelete(map);
                }
                await _uow.SaveChangesAsync();

                // 2) Cập nhật meta quiz
                quiz.CourseId = dto.CourseId;
                quiz.Title = dto.Title;
                quiz.TotalQuestions = dto.Questions.Count;
                _uow.QuizRepository.Update(quiz);
                await _uow.SaveChangesAsync();

                // 3) Thêm bộ câu hỏi/mapping mới
                int order = 1;
                foreach (var qDto in dto.Questions)
                {
                    var question = new Question
                    {
                        CourseId = dto.CourseId,
                        Type = qDto.Type,
                        Stem = qDto.Stem,
                        Explanation = qDto.Explanation,
                        Difficulty = qDto.Difficulty
                    };
                    await _uow.QuestionRepository.AddAsync(question);
                    await _uow.SaveChangesAsync();

                    foreach (var op in qDto.Options)
                    {
                        await _uow.QuestionOptionRepository.AddAsync(new QuestionOption
                        {
                            QuestionId = question.Id,
                            OptionText = op.Text,
                            IsCorrect = op.IsCorrect
                        });
                    }
                    await _uow.SaveChangesAsync();

                    await _uow.QuizQuestionRepository.AddAsync(new QuizQuestion
                    {
                        QuizId = quiz.Id,
                        QuestionId = question.Id,
                        Points = qDto.Points <= 0 ? 1 : qDto.Points,
                        OrderIndex = order++
                    });
                    await _uow.SaveChangesAsync();
                }

                await tx.CommitAsync();
                return await GetDetailOk(quiz.Id, 200);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Update quiz failed");
                return Result<QuizDetailDTO>.Fail("Update quiz failed.", 500)
                    .AddError("exception", ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteAsync(int quizId)
        {
            var quiz = await _uow.QuizRepository.GetByIdAsync(quizId);
            if (quiz == null) return Result<bool>.Fail("Quiz not found.", 404);

            using var tx = await _uow.BeginTransactionAsync();
            try
            {
                // soft delete mapping + question + options + quiz
                var maps = _uow.QuizQuestionRepository
                    .GetAllQueryable(nameof(QuizQuestion.Question) + "," + nameof(QuizQuestion.Question) + "." + nameof(Question.QuestionOptions))
                    .Where(x => x.QuizId == quizId).ToList();

                foreach (var m in maps)
                {
                    foreach (var op in m.Question.QuestionOptions) _uow.QuestionOptionRepository.SoftDelete(op);
                    _uow.QuestionRepository.SoftDelete(m.Question);
                    _uow.QuizQuestionRepository.SoftDelete(m);
                }

                _uow.QuizRepository.SoftDelete(quiz);
                await _uow.SaveChangesAsync();
                await tx.CommitAsync();

                return Result<bool>.Success(true, "Deleted", 200);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Delete quiz failed");
                return Result<bool>.Fail("Delete quiz failed.", 500).AddError("exception", ex.Message);
            }
        }

        // helper trả detail theo chuẩn wrapper
        private async Task<Result<QuizDetailDTO>> GetDetailOk(int quizId, int status)
        {
            var detail = await GetDetailAsync(quizId);
            detail.StatusCode = status;
            return detail;
        }
    }
}
