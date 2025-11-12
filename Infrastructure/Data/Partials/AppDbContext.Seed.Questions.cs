using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public partial class AppDbContext
    {
        private static void ConfigureQuestions(ModelBuilder modelBuilder)
        {
            const int quizzesToSeed = 50;
            const int questionsPerQuiz = 5;
            var baseDate = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc);
            var topics = new[]
            {
                "algebra fundamentals",
                "graph interpretation",
                "experimental setup",
                "data analysis",
                "concept recall",
                "problem solving",
                "application scenario"
            };

            var questions = new List<object>(quizzesToSeed * questionsPerQuiz);
            var quizQuestions = new List<object>(quizzesToSeed * questionsPerQuiz);
            var questionOptions = new List<object>(quizzesToSeed * questionsPerQuiz * 4);

            var questionId = 1;
            var quizQuestionId = 1;
            var optionId = 1;

            for (var quizId = 1; quizId <= quizzesToSeed; quizId++)
            {
                // Course seeding creates two quizzes per course in order
                var courseId = ((quizId - 1) / 2) + 1;

                for (var order = 1; order <= questionsPerQuiz; order++)
                {
                    var createdAt = baseDate.AddDays(quizId).AddMinutes(order * 3);
                    var topic = topics[(quizId + order) % topics.Length];

                    questions.Add(new
                    {
                        Id = questionId,
                        CourseId = courseId,
                        Type = QuestionType.MultipleChoice,
                        Stem = $"Quiz {quizId} check {order}: {topic}.",
                        Explanation = $"Review the lesson summary for quiz {quizId} question {order}.",
                        Difficulty = (byte)(1 + ((quizId + order) % 5)),
                        CreatedAt = createdAt,
                        CreatedBy = "Seed",
                        ModifiedAt = createdAt,
                        ModifiedBy = "Seed",
                        IsDeleted = false
                    });

                    quizQuestions.Add(new
                    {
                        Id = quizQuestionId,
                        QuizId = quizId,
                        QuestionId = questionId,
                        Points = Math.Round(1.0m + (order - 1) * 0.25m, 2),
                        OrderIndex = order,
                        CreatedAt = createdAt,
                        CreatedBy = "Seed",
                        ModifiedAt = createdAt,
                        ModifiedBy = "Seed",
                        IsDeleted = false
                    });

                    var correctIndex = (quizId + order) % 4;
                    var choiceLabels = new[] { "A", "B", "C", "D" };
                    for (var opt = 0; opt < 4; opt++)
                    {
                        var optionCreated = createdAt.AddSeconds(opt + 1);
                        questionOptions.Add(new
                        {
                            Id = optionId,
                            QuestionId = questionId,
                            OptionText = $"{choiceLabels[opt]}: {topic} scenario {opt + 1} for quiz {quizId}.",
                            IsCorrect = opt == correctIndex,
                            CreatedAt = optionCreated,
                            CreatedBy = "Seed",
                            ModifiedAt = optionCreated,
                            ModifiedBy = "Seed",
                            IsDeleted = false
                        });
                        optionId++;
                    }

                    questionId++;
                    quizQuestionId++;
                }
            }

            modelBuilder.Entity<Question>().HasData(questions.ToArray());
            modelBuilder.Entity<QuizQuestion>().HasData(quizQuestions.ToArray());
            modelBuilder.Entity<QuestionOption>().HasData(questionOptions.ToArray());
        }
    }
}
