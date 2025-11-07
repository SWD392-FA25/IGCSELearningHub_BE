using Application.DTOs.Quiz;
using Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Application.Mapping
{
    public static class QuizProjections
    {
        public static readonly Expression<Func<Quiz, QuizSummaryDTO>> Summary =
            quiz => new QuizSummaryDTO
            {
                Id = quiz.Id,
                CourseId = quiz.CourseId,
                Title = quiz.Title,
                TotalQuestions = quiz.TotalQuestions ?? 0,
                CreatedAt = quiz.CreatedAt
            };
    }
}
