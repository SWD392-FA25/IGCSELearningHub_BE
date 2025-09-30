using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class QuestionOption : BaseFullEntity
{
    public int QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual Question Question { get; set; } = null!;
}
