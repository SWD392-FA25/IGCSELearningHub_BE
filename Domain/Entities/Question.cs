using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Question : BaseFullEntity
{
    public int CourseId { get; set; }

    public QuestionType Type { get; set; } = QuestionType.MultipleChoice;

    public string Stem { get; set; } = null!;

    public string? Explanation { get; set; }

    public byte? Difficulty { get; set; }

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
}
