using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public partial class Account : BaseFullEntity
{
    public string UserName { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    public AccountRole Role { get; set; } = AccountRole.Student;

    public string Gender { get; set; } = string.Empty;

    public string? Status { get; set; }

    public bool IsExternal { get; set; } = false;

    public string? ExternalProvider { get; set; }

    public string? ExternalId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<LivestreamRegistration> LivestreamRegistrations { get; set; } = new List<LivestreamRegistration>();

    public virtual ICollection<Livestream> Livestreams { get; set; } = new List<Livestream>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
