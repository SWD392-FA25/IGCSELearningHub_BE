using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Submission : BaseFullEntity
{
    public int AssignmentId { get; set; }

    public int AccountId { get; set; }

    public decimal? Score { get; set; }

    public DateTime SubmittedDate { get; set; }

    public virtual Assignment Assignment { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;
}
