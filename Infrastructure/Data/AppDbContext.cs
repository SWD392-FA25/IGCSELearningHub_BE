using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AttemptAnswer> AttemptAnswers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CoursePackage> CoursePackages { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Livestream> Livestreams { get; set; }

    public virtual DbSet<LivestreamRegistration> LivestreamRegistrations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonCompletion> LessonCompletions { get; set; }

    public virtual DbSet<Progress> Progresses { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizAttempt> QuizAttempts { get; set; }

    public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<decimal>().HavePrecision(18, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ==== FIX Multiple Cascade Paths ====

        // Livestreams -> Accounts (TeacherId): KHÔNG cascade
        modelBuilder.Entity<Livestream>()
            .HasOne(l => l.Teacher)                  // nav tới Account
            .WithMany()                              // hoặc .WithMany(a => a.TeachingLivestreams)
            .HasForeignKey(l => l.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);      // QUAN TRỌNG

        // Livestreams -> Courses: giữ cascade (hợp lý nếu xoá Course thì xoá Livestream của nó)
        modelBuilder.Entity<Livestream>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Livestreams)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // LivestreamRegistrations -> Livestreams: giữ cascade
        modelBuilder.Entity<LivestreamRegistration>()
            .HasOne(r => r.Livestream)
            .WithMany(l => l.LivestreamRegistrations)
            .HasForeignKey(r => r.LivestreamId)
            .OnDelete(DeleteBehavior.Cascade);

        // LivestreamRegistrations -> Accounts (User đăng ký): KHÔNG cascade
        modelBuilder.Entity<LivestreamRegistration>()
            .HasOne(r => r.Account)
            .WithMany(a => a.LivestreamRegistrations)
            .HasForeignKey(r => r.AccountId)
            .OnDelete(DeleteBehavior.Restrict);      // QUAN TRỌNG

        // ==== Các quan hệ khác (giữ mặc định hợp lý) ====

        // Enrollments: nếu xoá Course/Account => xoá Enrollment
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Account)
            .WithMany(a => a.Enrollments)
            .HasForeignKey(e => e.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        // Progress -> Enrollment: cascade
        modelBuilder.Entity<Progress>()
            .HasOne(p => p.Enrollment)
            .WithMany(e => e.Progresses)
            .HasForeignKey(p => p.EnrollmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Assignments -> Courses: cascade
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Course)
            .WithMany(c => c.Assignments)
            .HasForeignKey(a => a.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Units -> Courses
        modelBuilder.Entity<Unit>()
            .HasOne(unit => unit.Course)
            .WithMany(c => c.Units)
            .HasForeignKey(unit => unit.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Unit>()
            .Property(unit => unit.Title)
            .IsRequired();

        // Lessons -> Unit; redundant FK to Course for quick access
        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Unit)
            .WithMany(unit => unit.Lessons)
            .HasForeignKey(l => l.UnitId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Course)
            .WithMany()
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Lesson>()
            .Property(l => l.Title)
            .IsRequired();

        // LessonCompletions -> Enrollment/Lesson
        modelBuilder.Entity<LessonCompletion>()
            .HasOne(lc => lc.Enrollment)
            .WithMany()
            .HasForeignKey(lc => lc.EnrollmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LessonCompletion>()
            .HasOne(lc => lc.Lesson)
            .WithMany()
            .HasForeignKey(lc => lc.LessonId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LessonCompletion>()
            .HasIndex(lc => new { lc.EnrollmentId, lc.LessonId })
            .IsUnique();

        // Submissions -> Assignment/Account: cascade
        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Assignment)
            .WithMany(a => a.Submissions)
            .HasForeignKey(s => s.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Account)
            .WithMany(a => a.Submissions)
            .HasForeignKey(s => s.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        // Quizzes -> Courses: cascade
        modelBuilder.Entity<Quiz>()
            .HasOne(q => q.Course)
            .WithMany(c => c.Quizzes)
            .HasForeignKey(q => q.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Questions -> Courses: cascade
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Course)
            .WithMany(c => c.Questions)
            .HasForeignKey(q => q.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // QuestionOptions -> Questions: cascade
        modelBuilder.Entity<QuestionOption>()
            .HasOne(o => o.Question)
            .WithMany(q => q.QuestionOptions)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // QuizQuestions -> Quiz/Question: cascade
        modelBuilder.Entity<QuizQuestion>()
            .HasOne(qq => qq.Quiz)
            .WithMany(q => q.QuizQuestions)
            .HasForeignKey(qq => qq.QuizId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<QuizQuestion>()
            .HasOne(qq => qq.Question)
            .WithMany(q => q.QuizQuestions)
            .HasForeignKey(qq => qq.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // QuizAttempts -> Quiz/Account: cascade
        modelBuilder.Entity<QuizAttempt>()
            .HasOne(a => a.Quiz)
            .WithMany(q => q.QuizAttempts)
            .HasForeignKey(a => a.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuizAttempt>()
            .HasOne(a => a.Account)
            .WithMany(u => u.QuizAttempts)
            .HasForeignKey(a => a.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        // AttemptAnswers -> Attempt/Question/SelectedOption: cascade
        modelBuilder.Entity<AttemptAnswer>()
            .HasOne(x => x.Attempt)
            .WithMany(a => a.AttemptAnswers)
            .HasForeignKey(x => x.AttemptId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AttemptAnswer>()
            .HasOne(x => x.Question)
            .WithMany(q => q.AttemptAnswers)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AttemptAnswer>()
            .HasOne(x => x.SelectedOption)
            .WithMany(o => o.AttemptAnswers)
            .HasForeignKey(x => x.SelectedOptionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Orders -> Accounts: cascade
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Account)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderDetails -> Orders: cascade
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Payments -> Orders/PaymentMethods: cascade
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.PaymentMethod)
            .WithMany(pm => pm.Payments)
            .HasForeignKey(p => p.PaymentMethodId)
            .OnDelete(DeleteBehavior.Cascade);

        // RefreshTokens -> Accounts: cascade
        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.Account)
            .WithMany() // optional: add navigation collection on Account if needed
            .HasForeignKey(rt => rt.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        // ==== Precision đặc thù (nếu muốn khác 18,2) ====
        modelBuilder.Entity<QuizAttempt>().Property(x => x.Score).HasPrecision(18, 4);
        modelBuilder.Entity<QuizQuestion>().Property(x => x.Points).HasPrecision(18, 4);
        modelBuilder.Entity<AttemptAnswer>().Property(x => x.PointsAwarded).HasPrecision(18, 4);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
