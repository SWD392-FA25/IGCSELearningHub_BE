using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AttemptAnswer : BaseFullEntity
{
    public int AttemptId { get; set; }

    public int QuestionId { get; set; }

    public int? SelectedOptionId { get; set; }

    public bool IsCorrect { get; set; }

    public decimal? PointsAwarded { get; set; }

    public virtual QuizAttempt Attempt { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual QuestionOption? SelectedOption { get; set; }
}
