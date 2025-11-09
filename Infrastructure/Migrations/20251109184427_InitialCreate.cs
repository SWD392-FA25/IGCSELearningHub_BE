using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExternal = table.Column<bool>(type: "bit", nullable: false),
                    ExternalProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoursePackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectGroup = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethodDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    DeviceToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OsVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseCoursePackage",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    PackagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCoursePackage", x => new { x.CoursesId, x.PackagesId });
                    table.ForeignKey(
                        name: "FK_CourseCoursePackage_CoursePackages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "CoursePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCoursePackage_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livestreams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livestreams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livestreams_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Livestreams_Accounts_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Livestreams_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Stem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Difficulty = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    CompletedPercent = table.Column<byte>(type: "tinyint", nullable: false),
                    LastAccessDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progresses_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivestreamRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivestreamId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivestreamRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivestreamRegistrations_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LivestreamRegistrations_Livestreams_LivestreamId",
                        column: x => x.LivestreamId,
                        principalTable: "Livestreams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    AttemptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    IsFreePreview = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lessons_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttemptAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttemptId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SelectedOptionId = table.Column<int>(type: "int", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    PointsAwarded = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttemptAnswers_QuestionOptions_SelectedOptionId",
                        column: x => x.SelectedOptionId,
                        principalTable: "QuestionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttemptAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttemptAnswers_QuizAttempts_AttemptId",
                        column: x => x.AttemptId,
                        principalTable: "QuizAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonCompletions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonCompletions_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonCompletions_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "ExternalId", "ExternalProvider", "FullName", "Gender", "IsDeleted", "IsExternal", "ModifiedAt", "ModifiedBy", "Password", "PhoneNumber", "Role", "Status", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 2, 0, 0, 0, DateTimeKind.Utc), "Seed", "owen.tran@igcsehub.com", null, null, "Owen Tran", "Male", false, false, new DateTime(2024, 1, 1, 2, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987000001", 2, "Active", "admin.owen" },
                    { 2, new DateTime(2024, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Seed", "linh.pham@igcsehub.com", null, null, "Linh Pham", "Female", false, false, new DateTime(2024, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987000002", 2, "Active", "admin.linh" },
                    { 3, new DateTime(2024, 1, 1, 4, 0, 0, 0, DateTimeKind.Utc), "Seed", "michael.le@igcsehub.com", null, null, "Michael Le", "Male", false, false, new DateTime(2024, 1, 1, 4, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987000003", 2, "Active", "admin.michael" },
                    { 4, new DateTime(2024, 1, 1, 5, 0, 0, 0, DateTimeKind.Utc), "Seed", "anh.nguyen@igcsehub.com", null, null, "Anh Nguyen", "Female", false, false, new DateTime(2024, 1, 1, 5, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987000004", 2, "Active", "admin.anh" },
                    { 5, new DateTime(2024, 1, 1, 6, 0, 0, 0, DateTimeKind.Utc), "Seed", "olivia.vu@igcsehub.com", null, null, "Olivia Vu", "Female", false, false, new DateTime(2024, 1, 1, 6, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987000005", 2, "Active", "admin.olivia" },
                    { 6, new DateTime(2024, 1, 1, 7, 0, 0, 0, DateTimeKind.Utc), "Seed", "mai.hoang@igcsehub.com", null, null, "Mai Hoang", "Female", false, false, new DateTime(2024, 1, 1, 7, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987111101", 1, "Active", "teacher.mai" },
                    { 7, new DateTime(2024, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc), "Seed", "james.dao@igcsehub.com", null, null, "James Dao", "Male", false, false, new DateTime(2024, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987111102", 1, "Active", "teacher.james" },
                    { 8, new DateTime(2024, 1, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Seed", "khang.bui@igcsehub.com", null, null, "Khang Bui", "Male", false, false, new DateTime(2024, 1, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987111103", 1, "Active", "teacher.khang" },
                    { 9, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Seed", "rachel.do@igcsehub.com", null, null, "Rachel Do", "Female", false, false, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987111104", 1, "Active", "teacher.rachel" },
                    { 10, new DateTime(2024, 1, 1, 11, 0, 0, 0, DateTimeKind.Utc), "Seed", "sophia.truong@igcsehub.com", null, null, "Sophia Truong", "Female", false, false, new DateTime(2024, 1, 1, 11, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987111105", 1, "Active", "teacher.sophia" },
                    { 11, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Seed", "huy.nguyen@student.igcsehub.com", null, null, "Huy Nguyen", "Male", false, false, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222201", 0, "Active", "student.huy" },
                    { 12, new DateTime(2024, 1, 1, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", "emma.phan@student.igcsehub.com", null, null, "Emma Phan", "Female", false, false, new DateTime(2024, 1, 1, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222202", 0, "Active", "student.emma" },
                    { 13, new DateTime(2024, 1, 1, 14, 0, 0, 0, DateTimeKind.Utc), "Seed", "minhchau.vo@student.igcsehub.com", null, null, "Minh Chau Vo", "Female", false, false, new DateTime(2024, 1, 1, 14, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222203", 0, "Active", "student.minhchau" },
                    { 14, new DateTime(2024, 1, 1, 15, 0, 0, 0, DateTimeKind.Utc), "Seed", "lucas.tran@student.igcsehub.com", null, null, "Lucas Tran", "Male", false, false, new DateTime(2024, 1, 1, 15, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222204", 0, "Active", "student.lucas" },
                    { 15, new DateTime(2024, 1, 1, 16, 0, 0, 0, DateTimeKind.Utc), "Seed", "hana.le@student.igcsehub.com", null, null, "Hana Le", "Female", false, false, new DateTime(2024, 1, 1, 16, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222205", 0, "Active", "student.hana" },
                    { 16, new DateTime(2024, 1, 1, 17, 0, 0, 0, DateTimeKind.Utc), "Seed", "david.ho@student.igcsehub.com", null, null, "David Ho", "Male", false, false, new DateTime(2024, 1, 1, 17, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222206", 0, "Active", "student.david" },
                    { 17, new DateTime(2024, 1, 1, 18, 0, 0, 0, DateTimeKind.Utc), "Seed", "quynh.nguyen@student.igcsehub.com", null, null, "Quynh Nguyen", "Female", false, false, new DateTime(2024, 1, 1, 18, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222207", 0, "Active", "student.quynh" },
                    { 18, new DateTime(2024, 1, 1, 19, 0, 0, 0, DateTimeKind.Utc), "Seed", "ryan.pham@student.igcsehub.com", null, null, "Ryan Pham", "Male", false, false, new DateTime(2024, 1, 1, 19, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222208", 0, "Active", "student.ryan" },
                    { 19, new DateTime(2024, 1, 1, 20, 0, 0, 0, DateTimeKind.Utc), "Seed", "julia.dang@student.igcsehub.com", null, null, "Julia Dang", "Female", false, false, new DateTime(2024, 1, 1, 20, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222209", 0, "Active", "student.julia" },
                    { 20, new DateTime(2024, 1, 1, 21, 0, 0, 0, DateTimeKind.Utc), "Seed", "long.truong@student.igcsehub.com", null, null, "Long Truong", "Male", false, false, new DateTime(2024, 1, 1, 21, 0, 0, 0, DateTimeKind.Utc), "Seed", "$2a$12$K1pL9sQ7wX3zD5fG8hJj9uoFBHHwT1ssWWuiS2bVg.tokPd1dCJL.", "0987222210", 0, "Active", "student.long" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Info", "IsDeleted", "Level", "ModifiedAt", "ModifiedBy", "Price", "SubjectGroup", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics curriculum covering algebra, geometry and statistics (module 1).", "Detailed overview for Mathematics module 1.", false, "Foundation", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2350000m, 4, "IGCSE Mathematics 1" },
                    { 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics curriculum covering algebra, geometry and statistics (module 2).", "Detailed overview for Mathematics module 2.", false, "Intermediate", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1950000m, 4, "IGCSE Mathematics 2" },
                    { 3, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics curriculum covering algebra, geometry and statistics (module 3).", "Detailed overview for Mathematics module 3.", false, "Advanced", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1950000m, 4, "IGCSE Mathematics 3" },
                    { 4, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics curriculum covering algebra, geometry and statistics (module 4).", "Detailed overview for Mathematics module 4.", false, "Advanced", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1950000m, 4, "IGCSE Mathematics 4" },
                    { 5, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics curriculum covering algebra, geometry and statistics (module 5).", "Detailed overview for Mathematics module 5.", false, "Intermediate", new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2350000m, 4, "IGCSE Mathematics 5" },
                    { 6, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics curriculum covering algebra, geometry and statistics (module 6).", "Detailed overview for Mathematics module 6.", false, "Intermediate", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1950000m, 4, "IGCSE Mathematics 6" },
                    { 7, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics curriculum covering mechanics, waves, electricity and nuclear science (module 1).", "Detailed overview for Physics module 1.", false, "Foundation", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2650000m, 3, "IGCSE Physics 1" },
                    { 8, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics curriculum covering mechanics, waves, electricity and nuclear science (module 2).", "Detailed overview for Physics module 2.", false, "Intermediate", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2150000m, 3, "IGCSE Physics 2" },
                    { 9, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics curriculum covering mechanics, waves, electricity and nuclear science (module 3).", "Detailed overview for Physics module 3.", false, "Advanced", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2250000m, 3, "IGCSE Physics 3" },
                    { 10, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics curriculum covering mechanics, waves, electricity and nuclear science (module 4).", "Detailed overview for Physics module 4.", false, "Advanced", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2650000m, 3, "IGCSE Physics 4" },
                    { 11, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics curriculum covering mechanics, waves, electricity and nuclear science (module 5).", "Detailed overview for Physics module 5.", false, "Foundation", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2650000m, 3, "IGCSE Physics 5" },
                    { 12, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics curriculum covering mechanics, waves, electricity and nuclear science (module 6).", "Detailed overview for Physics module 6.", false, "Advanced", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2650000m, 3, "IGCSE Physics 6" },
                    { 13, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry course covering atomic structure, bonding, energetics and kinetics (module 1).", "Detailed overview for Chemistry module 1.", false, "Foundation", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2350000m, 3, "IGCSE Chemistry 1" },
                    { 14, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry course covering atomic structure, bonding, energetics and kinetics (module 2).", "Detailed overview for Chemistry module 2.", false, "Intermediate", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2450000m, 3, "IGCSE Chemistry 2" },
                    { 15, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry course covering atomic structure, bonding, energetics and kinetics (module 3).", "Detailed overview for Chemistry module 3.", false, "Advanced", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2450000m, 3, "IGCSE Chemistry 3" },
                    { 16, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry course covering atomic structure, bonding, energetics and kinetics (module 4).", "Detailed overview for Chemistry module 4.", false, "Intermediate", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2750000m, 3, "IGCSE Chemistry 4" },
                    { 17, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry course covering atomic structure, bonding, energetics and kinetics (module 5).", "Detailed overview for Chemistry module 5.", false, "Foundation", new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2550000m, 3, "IGCSE Chemistry 5" },
                    { 18, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology course covering cell biology, genetics, ecology and evolution (module 1).", "Detailed overview for Biology module 1.", false, "Foundation", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2600000m, 3, "IGCSE Biology 1" },
                    { 19, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology course covering cell biology, genetics, ecology and evolution (module 2).", "Detailed overview for Biology module 2.", false, "Intermediate", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2100000m, 3, "IGCSE Biology 2" },
                    { 20, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology course covering cell biology, genetics, ecology and evolution (module 3).", "Detailed overview for Biology module 3.", false, "Advanced", new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2200000m, 3, "IGCSE Biology 3" },
                    { 21, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology course covering cell biology, genetics, ecology and evolution (module 4).", "Detailed overview for Biology module 4.", false, "Intermediate", new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2300000m, 3, "IGCSE Biology 4" },
                    { 22, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology course covering cell biology, genetics, ecology and evolution (module 5).", "Detailed overview for Biology module 5.", false, "Advanced", new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2300000m, 3, "IGCSE Biology 5" },
                    { 23, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English language course focusing on grammar, comprehension and writing (module 1).", "Detailed overview for English Language module 1.", false, "Foundation", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1850000m, 1, "IGCSE English Language 1" },
                    { 24, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English language course focusing on grammar, comprehension and writing (module 2).", "Detailed overview for English Language module 2.", false, "Intermediate", new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1850000m, 1, "IGCSE English Language 2" },
                    { 25, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English language course focusing on grammar, comprehension and writing (module 3).", "Detailed overview for English Language module 3.", false, "Advanced", new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1850000m, 1, "IGCSE English Language 3" },
                    { 26, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English language course focusing on grammar, comprehension and writing (module 4).", "Detailed overview for English Language module 4.", false, "Intermediate", new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1950000m, 1, "IGCSE English Language 4" },
                    { 27, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English language course focusing on grammar, comprehension and writing (module 5).", "Detailed overview for English Language module 5.", false, "Foundation", new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2250000m, 1, "IGCSE English Language 5" },
                    { 28, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Literature course analyzing prose, poetry and drama (module 1).", "Detailed overview for English Literature module 1.", false, "Foundation", new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2200000m, 1, "IGCSE English Literature 1" },
                    { 29, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Literature course analyzing prose, poetry and drama (module 2).", "Detailed overview for English Literature module 2.", false, "Intermediate", new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1900000m, 1, "IGCSE English Literature 2" },
                    { 30, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Literature course analyzing prose, poetry and drama (module 3).", "Detailed overview for English Literature module 3.", false, "Advanced", new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1900000m, 1, "IGCSE English Literature 3" },
                    { 31, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Literature course analyzing prose, poetry and drama (module 4).", "Detailed overview for English Literature module 4.", false, "Intermediate", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2100000m, 1, "IGCSE English Literature 4" },
                    { 32, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Literature course analyzing prose, poetry and drama (module 5).", "Detailed overview for English Literature module 5.", false, "Advanced", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2000000m, 1, "IGCSE English Literature 5" },
                    { 33, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History course covering world history and modern developments (module 1).", "Detailed overview for History module 1.", false, "Foundation", new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2000000m, 2, "IGCSE History 1" },
                    { 34, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History course covering world history and modern developments (module 2).", "Detailed overview for History module 2.", false, "Intermediate", new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2200000m, 2, "IGCSE History 2" },
                    { 35, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History course covering world history and modern developments (module 3).", "Detailed overview for History module 3.", false, "Advanced", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1700000m, 2, "IGCSE History 3" },
                    { 36, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History course covering world history and modern developments (module 4).", "Detailed overview for History module 4.", false, "Intermediate", new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1900000m, 2, "IGCSE History 4" },
                    { 37, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History course covering world history and modern developments (module 5).", "Detailed overview for History module 5.", false, "Intermediate", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2000000m, 2, "IGCSE History 5" },
                    { 38, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography course covering physical and human geography (module 1).", "Detailed overview for Geography module 1.", false, "Foundation", new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2150000m, 2, "IGCSE Geography 1" },
                    { 39, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography course covering physical and human geography (module 2).", "Detailed overview for Geography module 2.", false, "Intermediate", new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1850000m, 2, "IGCSE Geography 2" },
                    { 40, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography course covering physical and human geography (module 3).", "Detailed overview for Geography module 3.", false, "Advanced", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1650000m, 2, "IGCSE Geography 3" },
                    { 41, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography course covering physical and human geography (module 4).", "Detailed overview for Geography module 4.", false, "Foundation", new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1850000m, 2, "IGCSE Geography 4" },
                    { 42, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography course covering physical and human geography (module 5).", "Detailed overview for Geography module 5.", false, "Foundation", new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1750000m, 2, "IGCSE Geography 5" },
                    { 43, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics course covering microeconomics, macroeconomics and development (module 1).", "Detailed overview for Economics module 1.", false, "Foundation", new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2050000m, 2, "IGCSE Economics 1" },
                    { 44, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics course covering microeconomics, macroeconomics and development (module 2).", "Detailed overview for Economics module 2.", false, "Intermediate", new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2150000m, 2, "IGCSE Economics 2" },
                    { 45, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics course covering microeconomics, macroeconomics and development (module 3).", "Detailed overview for Economics module 3.", false, "Advanced", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2350000m, 2, "IGCSE Economics 3" },
                    { 46, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics course covering microeconomics, macroeconomics and development (module 4).", "Detailed overview for Economics module 4.", false, "Intermediate", new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2550000m, 2, "IGCSE Economics 4" },
                    { 47, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics course covering microeconomics, macroeconomics and development (module 5).", "Detailed overview for Economics module 5.", false, "Advanced", new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2050000m, 2, "IGCSE Economics 5" },
                    { 48, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business course covering entrepreneurship, marketing, finance and management (module 1).", "Detailed overview for Business Studies module 1.", false, "Foundation", new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2150000m, 5, "IGCSE Business Studies 1" },
                    { 49, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business course covering entrepreneurship, marketing, finance and management (module 2).", "Detailed overview for Business Studies module 2.", false, "Intermediate", new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2150000m, 5, "IGCSE Business Studies 2" },
                    { 50, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business course covering entrepreneurship, marketing, finance and management (module 3).", "Detailed overview for Business Studies module 3.", false, "Advanced", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2550000m, 5, "IGCSE Business Studies 3" },
                    { 51, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business course covering entrepreneurship, marketing, finance and management (module 4).", "Detailed overview for Business Studies module 4.", false, "Advanced", new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2550000m, 5, "IGCSE Business Studies 4" },
                    { 52, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business course covering entrepreneurship, marketing, finance and management (module 5).", "Detailed overview for Business Studies module 5.", false, "Foundation", new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2650000m, 5, "IGCSE Business Studies 5" },
                    { 53, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer science course covering programming, algorithms and data structures (module 1).", "Detailed overview for Computer Science module 1.", false, "Foundation", new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2550000m, 5, "IGCSE Computer Science 1" },
                    { 54, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer science course covering programming, algorithms and data structures (module 2).", "Detailed overview for Computer Science module 2.", false, "Intermediate", new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2450000m, 5, "IGCSE Computer Science 2" },
                    { 55, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer science course covering programming, algorithms and data structures (module 3).", "Detailed overview for Computer Science module 3.", false, "Advanced", new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2550000m, 5, "IGCSE Computer Science 3" },
                    { 56, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer science course covering programming, algorithms and data structures (module 4).", "Detailed overview for Computer Science module 4.", false, "Advanced", new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2650000m, 5, "IGCSE Computer Science 4" },
                    { 57, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer science course covering programming, algorithms and data structures (module 5).", "Detailed overview for Computer Science module 5.", false, "Foundation", new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2950000m, 5, "IGCSE Computer Science 5" },
                    { 58, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer science course covering programming, algorithms and data structures (module 6).", "Detailed overview for Computer Science module 6.", false, "Foundation", new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2550000m, 5, "IGCSE Computer Science 6" },
                    { 59, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & design course covering creative techniques and art history (module 1).", "Detailed overview for Art & Design module 1.", false, "Foundation", new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1900000m, 5, "IGCSE Art & Design 1" },
                    { 60, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & design course covering creative techniques and art history (module 2).", "Detailed overview for Art & Design module 2.", false, "Intermediate", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1900000m, 5, "IGCSE Art & Design 2" },
                    { 61, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & design course covering creative techniques and art history (module 3).", "Detailed overview for Art & Design module 3.", false, "Advanced", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2200000m, 5, "IGCSE Art & Design 3" },
                    { 62, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & design course covering creative techniques and art history (module 4).", "Detailed overview for Art & Design module 4.", false, "Foundation", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2300000m, 5, "IGCSE Art & Design 4" },
                    { 63, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & design course covering creative techniques and art history (module 5).", "Detailed overview for Art & Design module 5.", false, "Intermediate", new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1800000m, 5, "IGCSE Art & Design 5" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedBy", "PaymentMethodDescription", "PaymentMethodName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", "VNPay payment gateway", "VNPay" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Thanh toán tiền mặt", "Cash" }
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "CourseId", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "ModifiedAt", "ModifiedBy", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Mathematics Project" },
                    { 2, 1, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Mathematics Project" },
                    { 3, 2, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Mathematics Project" },
                    { 4, 2, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Mathematics Project" },
                    { 5, 3, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Mathematics Project" },
                    { 6, 3, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Mathematics Project" },
                    { 7, 4, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Mathematics Project" },
                    { 8, 4, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Mathematics Project" },
                    { 9, 5, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Mathematics Project" },
                    { 10, 5, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Mathematics Project" },
                    { 11, 6, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Mathematics Project" },
                    { 12, 6, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Mathematics Project" },
                    { 13, 7, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Physics Project" },
                    { 14, 7, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Physics Project" },
                    { 15, 8, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Physics Project" },
                    { 16, 8, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Physics Project" },
                    { 17, 9, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Physics Project" },
                    { 18, 9, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Physics Project" },
                    { 19, 10, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Physics Project" },
                    { 20, 10, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Physics Project" },
                    { 21, 11, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Physics Project" },
                    { 22, 11, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Physics Project" },
                    { 23, 12, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Physics Project" },
                    { 24, 12, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Physics Project" },
                    { 25, 13, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Chemistry Project" },
                    { 26, 13, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Chemistry Project" },
                    { 27, 14, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Chemistry Project" },
                    { 28, 14, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Chemistry Project" },
                    { 29, 15, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Chemistry Project" },
                    { 30, 15, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Chemistry Project" },
                    { 31, 16, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Chemistry Project" },
                    { 32, 16, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Chemistry Project" },
                    { 33, 17, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Chemistry Project" },
                    { 34, 17, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Chemistry Project" },
                    { 35, 18, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Biology Project" },
                    { 36, 18, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Biology Project" },
                    { 37, 19, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Biology Project" },
                    { 38, 19, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Biology Project" },
                    { 39, 20, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Biology Project" },
                    { 40, 20, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Biology Project" },
                    { 41, 21, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Biology Project" },
                    { 42, 21, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Biology Project" },
                    { 43, 22, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Biology Project" },
                    { 44, 22, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Biology Project" },
                    { 45, 23, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 1 focusing on key skills.", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Language Project" },
                    { 46, 23, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 2 focusing on key skills.", false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Language Project" },
                    { 47, 24, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Language Project" },
                    { 48, 24, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Language Project" },
                    { 49, 25, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Language Project" },
                    { 50, 25, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Language Project" },
                    { 51, 26, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Language Project" },
                    { 52, 26, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Language Project" },
                    { 53, 27, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Language Project" },
                    { 54, 27, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Language Project" },
                    { 55, 28, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Literature Project" },
                    { 56, 28, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Literature Project" },
                    { 57, 29, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Literature Project" },
                    { 58, 29, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Literature Project" },
                    { 59, 30, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Literature Project" },
                    { 60, 30, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Literature Project" },
                    { 61, 31, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Literature Project" },
                    { 62, 31, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Literature Project" },
                    { 63, 32, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: English Literature Project" },
                    { 64, 32, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: English Literature Project" },
                    { 65, 33, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: History Project" },
                    { 66, 33, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: History Project" },
                    { 67, 34, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: History Project" },
                    { 68, 34, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: History Project" },
                    { 69, 35, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: History Project" },
                    { 70, 35, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: History Project" },
                    { 71, 36, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: History Project" },
                    { 72, 36, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: History Project" },
                    { 73, 37, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: History Project" },
                    { 74, 37, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: History Project" },
                    { 75, 38, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Geography Project" },
                    { 76, 38, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Geography Project" },
                    { 77, 39, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Geography Project" },
                    { 78, 39, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Geography Project" },
                    { 79, 40, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Geography Project" },
                    { 80, 40, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Geography Project" },
                    { 81, 41, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Geography Project" },
                    { 82, 41, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Geography Project" },
                    { 83, 42, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Geography Project" },
                    { 84, 42, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Geography Project" },
                    { 85, 43, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Economics Project" },
                    { 86, 43, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Economics Project" },
                    { 87, 44, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Economics Project" },
                    { 88, 44, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Economics Project" },
                    { 89, 45, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Economics Project" },
                    { 90, 45, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Economics Project" },
                    { 91, 46, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Economics Project" },
                    { 92, 46, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Economics Project" },
                    { 93, 47, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Economics Project" },
                    { 94, 47, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Economics Project" },
                    { 95, 48, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Business Studies Project" },
                    { 96, 48, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Business Studies Project" },
                    { 97, 49, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Business Studies Project" },
                    { 98, 49, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Business Studies Project" },
                    { 99, 50, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Business Studies Project" },
                    { 100, 50, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Business Studies Project" },
                    { 101, 51, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Business Studies Project" },
                    { 102, 51, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 2 focusing on key skills.", false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Business Studies Project" },
                    { 103, 52, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Business Studies Project" },
                    { 104, 52, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Business Studies Project" },
                    { 105, 53, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 1 focusing on key skills.", false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Computer Science Project" },
                    { 106, 53, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Computer Science Project" },
                    { 107, 54, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Computer Science Project" },
                    { 108, 54, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Computer Science Project" },
                    { 109, 55, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Computer Science Project" },
                    { 110, 55, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Computer Science Project" },
                    { 111, 56, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Computer Science Project" },
                    { 112, 56, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Computer Science Project" },
                    { 113, 57, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Computer Science Project" },
                    { 114, 57, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Computer Science Project" },
                    { 115, 58, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Computer Science Project" },
                    { 116, 58, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Computer Science Project" },
                    { 117, 59, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Art & Design Project" },
                    { 118, 59, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Art & Design Project" },
                    { 119, 60, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Art & Design Project" },
                    { 120, 60, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Art & Design Project" },
                    { 121, 61, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Art & Design Project" },
                    { 122, 61, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Art & Design Project" },
                    { 123, 62, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Art & Design Project" },
                    { 124, 62, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Art & Design Project" },
                    { 125, 63, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 1 focusing on key skills.", false, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 1: Art & Design Project" },
                    { 126, 63, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design assignment 2 focusing on key skills.", false, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Assignment 2: Art & Design Project" }
                });

            migrationBuilder.InsertData(
                table: "Livestreams",
                columns: new[] { "Id", "AccountId", "CourseId", "CreatedAt", "CreatedBy", "IsDeleted", "ModifiedAt", "ModifiedBy", "Price", "Schedule", "TeacherId", "Title" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2024, 3, 1, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 1, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", 150000m, new DateTime(2024, 3, 6, 13, 0, 0, 0, DateTimeKind.Utc), 6, "Live Workshop: Algebra Essentials" },
                    { 2, null, 2, new DateTime(2024, 3, 4, 15, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 4, 15, 0, 0, 0, DateTimeKind.Utc), "Seed", 180000m, new DateTime(2024, 3, 9, 15, 0, 0, 0, DateTimeKind.Utc), 7, "Physics Lab: Motion & Forces Q&A" },
                    { 3, null, 3, new DateTime(2024, 3, 7, 12, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 7, 12, 0, 0, 0, DateTimeKind.Utc), "Seed", 170000m, new DateTime(2024, 3, 12, 12, 0, 0, 0, DateTimeKind.Utc), 8, "Chemistry Experiments: Reaction Rates" },
                    { 4, null, 4, new DateTime(2024, 3, 10, 14, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 10, 14, 0, 0, 0, DateTimeKind.Utc), "Seed", 165000m, new DateTime(2024, 3, 15, 14, 0, 0, 0, DateTimeKind.Utc), 9, "Biology Clinic: Genetics Deep Dive" },
                    { 5, null, 5, new DateTime(2024, 3, 13, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 13, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", 155000m, new DateTime(2024, 3, 18, 13, 0, 0, 0, DateTimeKind.Utc), 10, "English Language: Writing for Impact" },
                    { 6, null, 6, new DateTime(2024, 3, 16, 11, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 16, 11, 0, 0, 0, DateTimeKind.Utc), "Seed", 145000m, new DateTime(2024, 3, 21, 11, 0, 0, 0, DateTimeKind.Utc), 7, "English Literature: Poetry Study Hall" },
                    { 7, null, 7, new DateTime(2024, 3, 19, 14, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 19, 14, 0, 0, 0, DateTimeKind.Utc), "Seed", 160000m, new DateTime(2024, 3, 24, 14, 0, 0, 0, DateTimeKind.Utc), 8, "History Roundtable: Cold War Events" },
                    { 8, null, 8, new DateTime(2024, 3, 22, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 22, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", 150000m, new DateTime(2024, 3, 27, 13, 0, 0, 0, DateTimeKind.Utc), 6, "Geography Spotlight: Climate Change" },
                    { 9, null, 9, new DateTime(2024, 3, 25, 15, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 25, 15, 0, 0, 0, DateTimeKind.Utc), "Seed", 175000m, new DateTime(2024, 3, 30, 15, 0, 0, 0, DateTimeKind.Utc), 9, "Economics Insights: Market Structures" },
                    { 10, null, 10, new DateTime(2024, 3, 28, 12, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 28, 12, 0, 0, 0, DateTimeKind.Utc), "Seed", 165000m, new DateTime(2024, 4, 2, 12, 0, 0, 0, DateTimeKind.Utc), 10, "Business Studies: Marketing Pitches" },
                    { 11, null, 11, new DateTime(2024, 3, 31, 14, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 31, 14, 0, 0, 0, DateTimeKind.Utc), "Seed", 190000m, new DateTime(2024, 4, 5, 14, 0, 0, 0, DateTimeKind.Utc), 6, "Computer Science: Algorithm Clinic" },
                    { 12, null, 12, new DateTime(2024, 4, 3, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 4, 3, 13, 0, 0, 0, DateTimeKind.Utc), "Seed", 140000m, new DateTime(2024, 4, 8, 13, 0, 0, 0, DateTimeKind.Utc), 7, "Art & Design Critique Session" }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "CourseId", "CreatedAt", "CreatedBy", "IsDeleted", "ModifiedAt", "ModifiedBy", "Title", "TotalQuestions" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 1", 13 },
                    { 2, 1, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 2", 12 },
                    { 3, 2, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 1", 10 },
                    { 4, 2, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 2", 10 },
                    { 5, 3, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 1", 12 },
                    { 6, 3, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 2", 10 },
                    { 7, 4, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 1", 11 },
                    { 8, 4, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 2", 15 },
                    { 9, 5, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 1", 13 },
                    { 10, 5, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 2", 15 },
                    { 11, 6, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 1", 10 },
                    { 12, 6, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Mathematics Quiz 2", 15 },
                    { 13, 7, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 1", 13 },
                    { 14, 7, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 2", 14 },
                    { 15, 8, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 1", 15 },
                    { 16, 8, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 2", 15 },
                    { 17, 9, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 1", 14 },
                    { 18, 9, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 2", 15 },
                    { 19, 10, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 1", 11 },
                    { 20, 10, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 2", 11 },
                    { 21, 11, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 1", 11 },
                    { 22, 11, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 2", 15 },
                    { 23, 12, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 1", 12 },
                    { 24, 12, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Physics Quiz 2", 12 },
                    { 25, 13, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 1", 12 },
                    { 26, 13, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 2", 13 },
                    { 27, 14, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 1", 13 },
                    { 28, 14, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 2", 14 },
                    { 29, 15, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 1", 15 },
                    { 30, 15, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 2", 13 },
                    { 31, 16, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 1", 13 },
                    { 32, 16, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 2", 10 },
                    { 33, 17, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 1", 11 },
                    { 34, 17, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Chemistry Quiz 2", 14 },
                    { 35, 18, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 1", 11 },
                    { 36, 18, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 2", 14 },
                    { 37, 19, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 1", 15 },
                    { 38, 19, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 2", 11 },
                    { 39, 20, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 1", 10 },
                    { 40, 20, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 2", 12 },
                    { 41, 21, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 1", 13 },
                    { 42, 21, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 2", 12 },
                    { 43, 22, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 1", 11 },
                    { 44, 22, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Biology Quiz 2", 12 },
                    { 45, 23, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 1", 15 },
                    { 46, 23, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 2", 10 },
                    { 47, 24, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 1", 11 },
                    { 48, 24, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 2", 13 },
                    { 49, 25, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 1", 10 },
                    { 50, 25, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 2", 15 },
                    { 51, 26, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 1", 15 },
                    { 52, 26, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 2", 15 },
                    { 53, 27, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 1", 12 },
                    { 54, 27, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Language Quiz 2", 14 },
                    { 55, 28, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 1", 12 },
                    { 56, 28, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 2", 13 },
                    { 57, 29, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 1", 15 },
                    { 58, 29, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 2", 14 },
                    { 59, 30, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 1", 15 },
                    { 60, 30, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 2", 11 },
                    { 61, 31, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 1", 14 },
                    { 62, 31, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 2", 11 },
                    { 63, 32, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 1", 14 },
                    { 64, 32, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "English Literature Quiz 2", 13 },
                    { 65, 33, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 1", 13 },
                    { 66, 33, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 2", 10 },
                    { 67, 34, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 1", 10 },
                    { 68, 34, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 2", 13 },
                    { 69, 35, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 1", 13 },
                    { 70, 35, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 2", 12 },
                    { 71, 36, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 1", 13 },
                    { 72, 36, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 2", 11 },
                    { 73, 37, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 1", 14 },
                    { 74, 37, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "History Quiz 2", 11 },
                    { 75, 38, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 1", 12 },
                    { 76, 38, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 2", 12 },
                    { 77, 39, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 1", 10 },
                    { 78, 39, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 2", 12 },
                    { 79, 40, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 1", 12 },
                    { 80, 40, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 2", 15 },
                    { 81, 41, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 1", 10 },
                    { 82, 41, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 2", 15 },
                    { 83, 42, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 1", 10 },
                    { 84, 42, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Geography Quiz 2", 15 },
                    { 85, 43, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 1", 15 },
                    { 86, 43, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 2", 13 },
                    { 87, 44, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 1", 15 },
                    { 88, 44, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 2", 14 },
                    { 89, 45, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 1", 14 },
                    { 90, 45, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 2", 13 },
                    { 91, 46, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 1", 11 },
                    { 92, 46, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 2", 15 },
                    { 93, 47, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 1", 14 },
                    { 94, 47, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Economics Quiz 2", 11 },
                    { 95, 48, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 1", 12 },
                    { 96, 48, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 2", 12 },
                    { 97, 49, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 1", 11 },
                    { 98, 49, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 2", 14 },
                    { 99, 50, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 1", 10 },
                    { 100, 50, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 2", 12 },
                    { 101, 51, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 1", 11 },
                    { 102, 51, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 2", 10 },
                    { 103, 52, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 1", 13 },
                    { 104, 52, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Business Studies Quiz 2", 10 },
                    { 105, 53, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 1", 12 },
                    { 106, 53, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 2", 10 },
                    { 107, 54, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 1", 14 },
                    { 108, 54, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 2", 14 },
                    { 109, 55, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 1", 11 },
                    { 110, 55, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 2", 13 },
                    { 111, 56, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 1", 12 },
                    { 112, 56, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 2", 15 },
                    { 113, 57, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 1", 11 },
                    { 114, 57, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 2", 10 },
                    { 115, 58, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 1", 12 },
                    { 116, 58, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Computer Science Quiz 2", 10 },
                    { 117, 59, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 1", 12 },
                    { 118, 59, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 2", 11 },
                    { 119, 60, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 1", 11 },
                    { 120, 60, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 2", 10 },
                    { 121, 61, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 1", 12 },
                    { 122, 61, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 2", 15 },
                    { 123, 62, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 1", 13 },
                    { 124, 62, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 2", 15 },
                    { 125, 63, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 1", 10 },
                    { 126, 63, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", false, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Art & Design Quiz 2", 10 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "CourseId", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "ModifiedAt", "ModifiedBy", "OrderIndex", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 1.", false, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Mathematics Module" },
                    { 2, 1, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 2.", false, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Mathematics Module" },
                    { 3, 1, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 3.", false, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Mathematics Module" },
                    { 4, 2, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 1.", false, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Mathematics Module" },
                    { 5, 2, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 2.", false, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Mathematics Module" },
                    { 6, 2, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 3.", false, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Mathematics Module" },
                    { 7, 3, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 1.", false, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Mathematics Module" },
                    { 8, 3, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 2.", false, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Mathematics Module" },
                    { 9, 3, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 3.", false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Mathematics Module" },
                    { 10, 3, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 4.", false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Mathematics Module" },
                    { 11, 4, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 1.", false, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Mathematics Module" },
                    { 12, 4, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 2.", false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Mathematics Module" },
                    { 13, 4, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 3.", false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Mathematics Module" },
                    { 14, 5, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 1.", false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Mathematics Module" },
                    { 15, 5, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 2.", false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Mathematics Module" },
                    { 16, 5, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 3.", false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Mathematics Module" },
                    { 17, 6, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 1.", false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Mathematics Module" },
                    { 18, 6, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 2.", false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Mathematics Module" },
                    { 19, 6, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Mathematics unit 3.", false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Mathematics Module" },
                    { 20, 7, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 1.", false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Physics Module" },
                    { 21, 7, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 2.", false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Physics Module" },
                    { 22, 7, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 3.", false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Physics Module" },
                    { 23, 8, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 1.", false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Physics Module" },
                    { 24, 8, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 2.", false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Physics Module" },
                    { 25, 8, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 3.", false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Physics Module" },
                    { 26, 8, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 4.", false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Physics Module" },
                    { 27, 9, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 1.", false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Physics Module" },
                    { 28, 9, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 2.", false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Physics Module" },
                    { 29, 9, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 3.", false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Physics Module" },
                    { 30, 10, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 1.", false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Physics Module" },
                    { 31, 10, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 2.", false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Physics Module" },
                    { 32, 10, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 3.", false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Physics Module" },
                    { 33, 10, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 4.", false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Physics Module" },
                    { 34, 11, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 1.", false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Physics Module" },
                    { 35, 11, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 2.", false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Physics Module" },
                    { 36, 11, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 3.", false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Physics Module" },
                    { 37, 12, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 1.", false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Physics Module" },
                    { 38, 12, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 2.", false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Physics Module" },
                    { 39, 12, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Physics unit 3.", false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Physics Module" },
                    { 40, 13, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 1.", false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Chemistry Module" },
                    { 41, 13, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 2.", false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Chemistry Module" },
                    { 42, 13, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 3.", false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Chemistry Module" },
                    { 43, 13, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 4.", false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Chemistry Module" },
                    { 44, 14, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 1.", false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Chemistry Module" },
                    { 45, 14, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 2.", false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Chemistry Module" },
                    { 46, 14, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 3.", false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Chemistry Module" },
                    { 47, 15, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 1.", false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Chemistry Module" },
                    { 48, 15, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 2.", false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Chemistry Module" },
                    { 49, 15, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 3.", false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Chemistry Module" },
                    { 50, 15, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 4.", false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Chemistry Module" },
                    { 51, 16, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 1.", false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Chemistry Module" },
                    { 52, 16, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 2.", false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Chemistry Module" },
                    { 53, 16, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 3.", false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Chemistry Module" },
                    { 54, 16, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 4.", false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Chemistry Module" },
                    { 55, 17, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 1.", false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Chemistry Module" },
                    { 56, 17, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 2.", false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Chemistry Module" },
                    { 57, 17, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Chemistry unit 3.", false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Chemistry Module" },
                    { 58, 18, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 1.", false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Biology Module" },
                    { 59, 18, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 2.", false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Biology Module" },
                    { 60, 18, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 3.", false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Biology Module" },
                    { 61, 18, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 4.", false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Biology Module" },
                    { 62, 19, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 1.", false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Biology Module" },
                    { 63, 19, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 2.", false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Biology Module" },
                    { 64, 19, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 3.", false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Biology Module" },
                    { 65, 20, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 1.", false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Biology Module" },
                    { 66, 20, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 2.", false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Biology Module" },
                    { 67, 20, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 3.", false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Biology Module" },
                    { 68, 21, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 1.", false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Biology Module" },
                    { 69, 21, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 2.", false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Biology Module" },
                    { 70, 21, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 3.", false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Biology Module" },
                    { 71, 22, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 1.", false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Biology Module" },
                    { 72, 22, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 2.", false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Biology Module" },
                    { 73, 22, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 3.", false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Biology Module" },
                    { 74, 22, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Biology unit 4.", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Biology Module" },
                    { 75, 23, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 1.", false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Language Module" },
                    { 76, 23, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 2.", false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Language Module" },
                    { 77, 23, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 3.", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Language Module" },
                    { 78, 24, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 1.", false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Language Module" },
                    { 79, 24, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 2.", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Language Module" },
                    { 80, 24, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 3.", false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Language Module" },
                    { 81, 24, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 4.", false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: English Language Module" },
                    { 82, 25, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 1.", false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Language Module" },
                    { 83, 25, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 2.", false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Language Module" },
                    { 84, 25, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 3.", false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Language Module" },
                    { 85, 25, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 4.", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: English Language Module" },
                    { 86, 26, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 1.", false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Language Module" },
                    { 87, 26, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 2.", false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Language Module" },
                    { 88, 26, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 3.", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Language Module" },
                    { 89, 27, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 1.", false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Language Module" },
                    { 90, 27, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 2.", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Language Module" },
                    { 91, 27, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 3.", false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Language Module" },
                    { 92, 27, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Language unit 4.", false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: English Language Module" },
                    { 93, 28, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 1.", false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Literature Module" },
                    { 94, 28, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 2.", false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Literature Module" },
                    { 95, 28, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 3.", false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Literature Module" },
                    { 96, 29, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 1.", false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Literature Module" },
                    { 97, 29, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 2.", false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Literature Module" },
                    { 98, 29, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 3.", false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Literature Module" },
                    { 99, 30, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 1.", false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Literature Module" },
                    { 100, 30, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 2.", false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Literature Module" },
                    { 101, 30, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 3.", false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Literature Module" },
                    { 102, 30, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 4.", false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: English Literature Module" },
                    { 103, 31, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 1.", false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Literature Module" },
                    { 104, 31, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 2.", false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Literature Module" },
                    { 105, 31, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 3.", false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Literature Module" },
                    { 106, 32, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 1.", false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: English Literature Module" },
                    { 107, 32, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 2.", false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: English Literature Module" },
                    { 108, 32, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 3.", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: English Literature Module" },
                    { 109, 32, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for English Literature unit 4.", false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: English Literature Module" },
                    { 110, 33, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 1.", false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: History Module" },
                    { 111, 33, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 2.", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: History Module" },
                    { 112, 33, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 3.", false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: History Module" },
                    { 113, 34, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 1.", false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: History Module" },
                    { 114, 34, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 2.", false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: History Module" },
                    { 115, 34, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 3.", false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: History Module" },
                    { 116, 34, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 4.", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: History Module" },
                    { 117, 35, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 1.", false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: History Module" },
                    { 118, 35, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 2.", false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: History Module" },
                    { 119, 35, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 3.", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: History Module" },
                    { 120, 36, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 1.", false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: History Module" },
                    { 121, 36, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 2.", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: History Module" },
                    { 122, 36, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 3.", false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: History Module" },
                    { 123, 36, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 4.", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: History Module" },
                    { 124, 37, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 1.", false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: History Module" },
                    { 125, 37, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 2.", false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: History Module" },
                    { 126, 37, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 3.", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: History Module" },
                    { 127, 37, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for History unit 4.", false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: History Module" },
                    { 128, 38, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 1.", false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Geography Module" },
                    { 129, 38, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 2.", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Geography Module" },
                    { 130, 38, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 3.", false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Geography Module" },
                    { 131, 38, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 4.", false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Geography Module" },
                    { 132, 39, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 1.", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Geography Module" },
                    { 133, 39, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 2.", false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Geography Module" },
                    { 134, 39, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 3.", false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Geography Module" },
                    { 135, 40, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 1.", false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Geography Module" },
                    { 136, 40, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 2.", false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Geography Module" },
                    { 137, 40, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 3.", false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Geography Module" },
                    { 138, 41, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 1.", false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Geography Module" },
                    { 139, 41, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 2.", false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Geography Module" },
                    { 140, 41, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 3.", false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Geography Module" },
                    { 141, 42, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 1.", false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Geography Module" },
                    { 142, 42, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 2.", false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Geography Module" },
                    { 143, 42, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 3.", false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Geography Module" },
                    { 144, 42, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Geography unit 4.", false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Geography Module" },
                    { 145, 43, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 1.", false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Economics Module" },
                    { 146, 43, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 2.", false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Economics Module" },
                    { 147, 43, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 3.", false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Economics Module" },
                    { 148, 43, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 4.", false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Economics Module" },
                    { 149, 44, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 1.", false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Economics Module" },
                    { 150, 44, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 2.", false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Economics Module" },
                    { 151, 44, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 3.", false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Economics Module" },
                    { 152, 45, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 1.", false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Economics Module" },
                    { 153, 45, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 2.", false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Economics Module" },
                    { 154, 45, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 3.", false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Economics Module" },
                    { 155, 46, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 1.", false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Economics Module" },
                    { 156, 46, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 2.", false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Economics Module" },
                    { 157, 46, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 3.", false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Economics Module" },
                    { 158, 47, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 1.", false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Economics Module" },
                    { 159, 47, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 2.", false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Economics Module" },
                    { 160, 47, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 3.", false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Economics Module" },
                    { 161, 47, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Economics unit 4.", false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Economics Module" },
                    { 162, 48, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 1.", false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Business Studies Module" },
                    { 163, 48, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 2.", false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Business Studies Module" },
                    { 164, 48, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 3.", false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Business Studies Module" },
                    { 165, 49, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 1.", false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Business Studies Module" },
                    { 166, 49, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 2.", false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Business Studies Module" },
                    { 167, 49, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 3.", false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Business Studies Module" },
                    { 168, 49, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 4.", false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Business Studies Module" },
                    { 169, 50, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 1.", false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Business Studies Module" },
                    { 170, 50, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 2.", false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Business Studies Module" },
                    { 171, 50, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 3.", false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Business Studies Module" },
                    { 172, 50, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 4.", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Business Studies Module" },
                    { 173, 51, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 1.", false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Business Studies Module" },
                    { 174, 51, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 2.", false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Business Studies Module" },
                    { 175, 51, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 3.", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Business Studies Module" },
                    { 176, 51, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 4.", false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Business Studies Module" },
                    { 177, 52, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 1.", false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Business Studies Module" },
                    { 178, 52, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 2.", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Business Studies Module" },
                    { 179, 52, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 3.", false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Business Studies Module" },
                    { 180, 52, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Business Studies unit 4.", false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Business Studies Module" },
                    { 181, 53, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 1.", false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Computer Science Module" },
                    { 182, 53, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 2.", false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Computer Science Module" },
                    { 183, 53, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 3.", false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Computer Science Module" },
                    { 184, 54, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 1.", false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Computer Science Module" },
                    { 185, 54, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 2.", false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Computer Science Module" },
                    { 186, 54, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 3.", false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Computer Science Module" },
                    { 187, 54, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 4.", false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Computer Science Module" },
                    { 188, 55, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 1.", false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Computer Science Module" },
                    { 189, 55, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 2.", false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Computer Science Module" },
                    { 190, 55, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 3.", false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Computer Science Module" },
                    { 191, 55, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 4.", false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Computer Science Module" },
                    { 192, 56, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 1.", false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Computer Science Module" },
                    { 193, 56, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 2.", false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Computer Science Module" },
                    { 194, 56, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 3.", false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Computer Science Module" },
                    { 195, 56, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 4.", false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Computer Science Module" },
                    { 196, 57, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 1.", false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Computer Science Module" },
                    { 197, 57, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 2.", false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Computer Science Module" },
                    { 198, 57, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 3.", false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Computer Science Module" },
                    { 199, 57, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 4.", false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 4, "Unit 4: Computer Science Module" },
                    { 200, 58, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 1.", false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Computer Science Module" },
                    { 201, 58, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 2.", false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Computer Science Module" },
                    { 202, 58, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Computer Science unit 3.", false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Computer Science Module" },
                    { 203, 59, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 1.", false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Art & Design Module" },
                    { 204, 59, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 2.", false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Art & Design Module" },
                    { 205, 59, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 3.", false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Art & Design Module" },
                    { 206, 60, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 1.", false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Art & Design Module" },
                    { 207, 60, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 2.", false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Art & Design Module" },
                    { 208, 60, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 3.", false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Art & Design Module" },
                    { 209, 61, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 1.", false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Art & Design Module" },
                    { 210, 61, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 2.", false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Art & Design Module" },
                    { 211, 61, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 3.", false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Art & Design Module" },
                    { 212, 62, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 1.", false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Art & Design Module" },
                    { 213, 62, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 2.", false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Art & Design Module" },
                    { 214, 62, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 3.", false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Art & Design Module" },
                    { 215, 63, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 1.", false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Unit 1: Art & Design Module" },
                    { 216, 63, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 2.", false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Unit 2: Art & Design Module" },
                    { 217, 63, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Focus topics for Art & Design unit 3.", false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Unit 3: Art & Design Module" }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "AttachmentUrl", "CourseId", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "IsFreePreview", "ModifiedAt", "ModifiedBy", "OrderIndex", "Title", "UnitId", "VideoUrl" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 1.", false, true, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Mathematics Essentials", 1, "https://cdn.example.com/videos/mathematics-1-1.mp4" },
                    { 2, "https://cdn.example.com/files/mathematics-1-2.pdf", 1, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 2.", false, false, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Mathematics Essentials", 1, null },
                    { 3, "https://cdn.example.com/files/mathematics-1-3.pdf", 1, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 3.", false, false, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Mathematics Essentials", 2, null },
                    { 4, null, 1, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 4.", false, false, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Mathematics Essentials", 2, null },
                    { 5, null, 1, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 5.", false, false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Mathematics Essentials", 2, "https://cdn.example.com/videos/mathematics-1-5.mp4" },
                    { 6, "https://cdn.example.com/files/mathematics-1-6.pdf", 1, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 6.", false, false, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Mathematics Essentials", 3, null },
                    { 7, "https://cdn.example.com/files/mathematics-1-7.pdf", 1, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 7.", false, false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Mathematics Essentials", 3, null },
                    { 8, "https://cdn.example.com/files/mathematics-2-1.pdf", 2, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 1.", false, true, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Mathematics Essentials", 4, "https://cdn.example.com/videos/mathematics-2-1.mp4" },
                    { 9, "https://cdn.example.com/files/mathematics-2-2.pdf", 2, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 2.", false, false, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Mathematics Essentials", 4, null },
                    { 10, "https://cdn.example.com/files/mathematics-2-3.pdf", 2, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 3.", false, false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Mathematics Essentials", 4, null },
                    { 11, null, 2, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 4.", false, false, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Mathematics Essentials", 5, "https://cdn.example.com/videos/mathematics-2-4.mp4" },
                    { 12, null, 2, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 5.", false, false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Mathematics Essentials", 5, null },
                    { 13, "https://cdn.example.com/files/mathematics-2-6.pdf", 2, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 6.", false, false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: Mathematics Essentials", 5, "https://cdn.example.com/videos/mathematics-2-6.mp4" },
                    { 14, "https://cdn.example.com/files/mathematics-2-7.pdf", 2, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 7.", false, false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Mathematics Essentials", 6, "https://cdn.example.com/videos/mathematics-2-7.mp4" },
                    { 15, null, 2, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 8.", false, false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Mathematics Essentials", 6, "https://cdn.example.com/videos/mathematics-2-8.mp4" },
                    { 16, null, 3, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 1.", false, true, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Mathematics Essentials", 7, null },
                    { 17, null, 3, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 2.", false, false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Mathematics Essentials", 7, "https://cdn.example.com/videos/mathematics-3-2.mp4" },
                    { 18, null, 3, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 3.", false, false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Mathematics Essentials", 7, "https://cdn.example.com/videos/mathematics-3-3.mp4" },
                    { 19, "https://cdn.example.com/files/mathematics-3-4.pdf", 3, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 4.", false, false, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Mathematics Essentials", 8, "https://cdn.example.com/videos/mathematics-3-4.mp4" },
                    { 20, null, 3, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 5.", false, false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Mathematics Essentials", 8, null },
                    { 21, null, 3, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 6.", false, false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Mathematics Essentials", 9, "https://cdn.example.com/videos/mathematics-3-6.mp4" },
                    { 22, null, 3, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 7.", false, false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Mathematics Essentials", 9, null },
                    { 23, null, 3, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 8.", false, false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Mathematics Essentials", 9, null },
                    { 24, null, 3, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 9.", false, false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 9: Mathematics Essentials", 10, null },
                    { 25, "https://cdn.example.com/files/mathematics-3-10.pdf", 3, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 10.", false, false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 10: Mathematics Essentials", 10, null },
                    { 26, "https://cdn.example.com/files/mathematics-3-11.pdf", 3, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 11.", false, false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 11: Mathematics Essentials", 10, null },
                    { 27, null, 4, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 1.", false, true, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Mathematics Essentials", 11, "https://cdn.example.com/videos/mathematics-4-1.mp4" },
                    { 28, "https://cdn.example.com/files/mathematics-4-2.pdf", 4, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 2.", false, false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Mathematics Essentials", 11, "https://cdn.example.com/videos/mathematics-4-2.mp4" },
                    { 29, "https://cdn.example.com/files/mathematics-4-3.pdf", 4, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 3.", false, false, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Mathematics Essentials", 12, null },
                    { 30, null, 4, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 4.", false, false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Mathematics Essentials", 12, null },
                    { 31, "https://cdn.example.com/files/mathematics-4-5.pdf", 4, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 5.", false, false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Mathematics Essentials", 13, null },
                    { 32, null, 4, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 6.", false, false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Mathematics Essentials", 13, "https://cdn.example.com/videos/mathematics-4-6.mp4" },
                    { 33, null, 5, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 1.", false, true, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Mathematics Essentials", 14, "https://cdn.example.com/videos/mathematics-5-1.mp4" },
                    { 34, null, 5, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 2.", false, false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Mathematics Essentials", 14, "https://cdn.example.com/videos/mathematics-5-2.mp4" },
                    { 35, null, 5, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 3.", false, false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Mathematics Essentials", 14, null },
                    { 36, null, 5, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 4.", false, false, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Mathematics Essentials", 15, "https://cdn.example.com/videos/mathematics-5-4.mp4" },
                    { 37, null, 5, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 5.", false, false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Mathematics Essentials", 15, null },
                    { 38, "https://cdn.example.com/files/mathematics-5-6.pdf", 5, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 6.", false, false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Mathematics Essentials", 16, "https://cdn.example.com/videos/mathematics-5-6.mp4" },
                    { 39, "https://cdn.example.com/files/mathematics-5-7.pdf", 5, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 7.", false, false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Mathematics Essentials", 16, "https://cdn.example.com/videos/mathematics-5-7.mp4" },
                    { 40, null, 5, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 8.", false, false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Mathematics Essentials", 16, "https://cdn.example.com/videos/mathematics-5-8.mp4" },
                    { 41, null, 6, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 1.", false, true, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Mathematics Essentials", 17, null },
                    { 42, null, 6, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 2.", false, false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Mathematics Essentials", 17, null },
                    { 43, "https://cdn.example.com/files/mathematics-6-3.pdf", 6, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 3.", false, false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Mathematics Essentials", 17, null },
                    { 44, "https://cdn.example.com/files/mathematics-6-4.pdf", 6, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 4.", false, false, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Mathematics Essentials", 18, null },
                    { 45, null, 6, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 5.", false, false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Mathematics Essentials", 18, null },
                    { 46, "https://cdn.example.com/files/mathematics-6-6.pdf", 6, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 6.", false, false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Mathematics Essentials", 19, null },
                    { 47, null, 6, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for mathematics lesson 7.", false, false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Mathematics Essentials", 19, "https://cdn.example.com/videos/mathematics-6-7.mp4" },
                    { 48, null, 7, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 1.", false, true, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Physics Essentials", 20, "https://cdn.example.com/videos/physics-1-1.mp4" },
                    { 49, "https://cdn.example.com/files/physics-1-2.pdf", 7, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 2.", false, false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Physics Essentials", 20, "https://cdn.example.com/videos/physics-1-2.mp4" },
                    { 50, null, 7, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 3.", false, false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Physics Essentials", 20, "https://cdn.example.com/videos/physics-1-3.mp4" },
                    { 51, null, 7, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 4.", false, false, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Physics Essentials", 21, null },
                    { 52, "https://cdn.example.com/files/physics-1-5.pdf", 7, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 5.", false, false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Physics Essentials", 21, null },
                    { 53, null, 7, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 6.", false, false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Physics Essentials", 22, "https://cdn.example.com/videos/physics-1-6.mp4" },
                    { 54, null, 7, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 7.", false, false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Physics Essentials", 22, "https://cdn.example.com/videos/physics-1-7.mp4" },
                    { 55, "https://cdn.example.com/files/physics-1-8.pdf", 7, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 8.", false, false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Physics Essentials", 22, null },
                    { 56, null, 8, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 1.", false, true, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Physics Essentials", 23, "https://cdn.example.com/videos/physics-2-1.mp4" },
                    { 57, "https://cdn.example.com/files/physics-2-2.pdf", 8, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 2.", false, false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Physics Essentials", 23, null },
                    { 58, "https://cdn.example.com/files/physics-2-3.pdf", 8, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 3.", false, false, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Physics Essentials", 24, "https://cdn.example.com/videos/physics-2-3.mp4" },
                    { 59, null, 8, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 4.", false, false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Physics Essentials", 24, "https://cdn.example.com/videos/physics-2-4.mp4" },
                    { 60, null, 8, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 5.", false, false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Physics Essentials", 25, null },
                    { 61, "https://cdn.example.com/files/physics-2-6.pdf", 8, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 6.", false, false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Physics Essentials", 25, "https://cdn.example.com/videos/physics-2-6.mp4" },
                    { 62, null, 8, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 7.", false, false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Physics Essentials", 26, "https://cdn.example.com/videos/physics-2-7.mp4" },
                    { 63, "https://cdn.example.com/files/physics-2-8.pdf", 8, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 8.", false, false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Physics Essentials", 26, null },
                    { 64, "https://cdn.example.com/files/physics-3-1.pdf", 9, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 1.", false, true, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Physics Essentials", 27, "https://cdn.example.com/videos/physics-3-1.mp4" },
                    { 65, "https://cdn.example.com/files/physics-3-2.pdf", 9, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 2.", false, false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Physics Essentials", 27, "https://cdn.example.com/videos/physics-3-2.mp4" },
                    { 66, null, 9, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 3.", false, false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Physics Essentials", 27, null },
                    { 67, null, 9, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 4.", false, false, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Physics Essentials", 28, "https://cdn.example.com/videos/physics-3-4.mp4" },
                    { 68, "https://cdn.example.com/files/physics-3-5.pdf", 9, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 5.", false, false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Physics Essentials", 28, "https://cdn.example.com/videos/physics-3-5.mp4" },
                    { 69, null, 9, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 6.", false, false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: Physics Essentials", 28, null },
                    { 70, "https://cdn.example.com/files/physics-3-7.pdf", 9, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 7.", false, false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Physics Essentials", 29, "https://cdn.example.com/videos/physics-3-7.mp4" },
                    { 71, null, 9, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 8.", false, false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Physics Essentials", 29, null },
                    { 72, "https://cdn.example.com/files/physics-4-1.pdf", 10, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 1.", false, true, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Physics Essentials", 30, "https://cdn.example.com/videos/physics-4-1.mp4" },
                    { 73, "https://cdn.example.com/files/physics-4-2.pdf", 10, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 2.", false, false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Physics Essentials", 30, null },
                    { 74, "https://cdn.example.com/files/physics-4-3.pdf", 10, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 3.", false, false, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Physics Essentials", 31, "https://cdn.example.com/videos/physics-4-3.mp4" },
                    { 75, null, 10, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 4.", false, false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Physics Essentials", 31, "https://cdn.example.com/videos/physics-4-4.mp4" },
                    { 76, "https://cdn.example.com/files/physics-4-5.pdf", 10, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 5.", false, false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Physics Essentials", 32, null },
                    { 77, null, 10, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 6.", false, false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Physics Essentials", 32, "https://cdn.example.com/videos/physics-4-6.mp4" },
                    { 78, "https://cdn.example.com/files/physics-4-7.pdf", 10, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 7.", false, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 7: Physics Essentials", 32, "https://cdn.example.com/videos/physics-4-7.mp4" },
                    { 79, "https://cdn.example.com/files/physics-4-8.pdf", 10, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 8.", false, false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Physics Essentials", 33, null },
                    { 80, null, 10, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 9.", false, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Physics Essentials", 33, null },
                    { 81, null, 10, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 10.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 10: Physics Essentials", 33, null },
                    { 82, "https://cdn.example.com/files/physics-5-1.pdf", 11, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 1.", false, true, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Physics Essentials", 34, null },
                    { 83, "https://cdn.example.com/files/physics-5-2.pdf", 11, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 2.", false, false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Physics Essentials", 34, "https://cdn.example.com/videos/physics-5-2.mp4" },
                    { 84, null, 11, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 3.", false, false, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Physics Essentials", 35, "https://cdn.example.com/videos/physics-5-3.mp4" },
                    { 85, "https://cdn.example.com/files/physics-5-4.pdf", 11, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 4.", false, false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Physics Essentials", 35, null },
                    { 86, null, 11, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 5.", false, false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Physics Essentials", 36, "https://cdn.example.com/videos/physics-5-5.mp4" },
                    { 87, null, 11, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 6.", false, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Physics Essentials", 36, "https://cdn.example.com/videos/physics-5-6.mp4" },
                    { 88, "https://cdn.example.com/files/physics-5-7.pdf", 11, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 7.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 7: Physics Essentials", 36, null },
                    { 89, null, 12, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 1.", false, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Physics Essentials", 37, "https://cdn.example.com/videos/physics-6-1.mp4" },
                    { 90, null, 12, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 2.", false, false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Physics Essentials", 37, "https://cdn.example.com/videos/physics-6-2.mp4" },
                    { 91, "https://cdn.example.com/files/physics-6-3.pdf", 12, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 3.", false, false, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Physics Essentials", 38, "https://cdn.example.com/videos/physics-6-3.mp4" },
                    { 92, "https://cdn.example.com/files/physics-6-4.pdf", 12, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 4.", false, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Physics Essentials", 38, null },
                    { 93, null, 12, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 5.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Physics Essentials", 38, "https://cdn.example.com/videos/physics-6-5.mp4" },
                    { 94, null, 12, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 6.", false, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Physics Essentials", 39, "https://cdn.example.com/videos/physics-6-6.mp4" },
                    { 95, "https://cdn.example.com/files/physics-6-7.pdf", 12, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for physics lesson 7.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Physics Essentials", 39, "https://cdn.example.com/videos/physics-6-7.mp4" },
                    { 96, null, 13, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 1.", false, true, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Chemistry Essentials", 40, "https://cdn.example.com/videos/chemistry-1-1.mp4" },
                    { 97, null, 13, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 2.", false, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Chemistry Essentials", 40, "https://cdn.example.com/videos/chemistry-1-2.mp4" },
                    { 98, "https://cdn.example.com/files/chemistry-1-3.pdf", 13, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 3.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Chemistry Essentials", 40, null },
                    { 99, null, 13, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 4.", false, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Chemistry Essentials", 41, "https://cdn.example.com/videos/chemistry-1-4.mp4" },
                    { 100, "https://cdn.example.com/files/chemistry-1-5.pdf", 13, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 5.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Chemistry Essentials", 41, null },
                    { 101, null, 13, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 6.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Chemistry Essentials", 42, null },
                    { 102, null, 13, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 7.", false, false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Chemistry Essentials", 42, "https://cdn.example.com/videos/chemistry-1-7.mp4" },
                    { 103, "https://cdn.example.com/files/chemistry-1-8.pdf", 13, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 8.", false, false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Chemistry Essentials", 43, null },
                    { 104, "https://cdn.example.com/files/chemistry-1-9.pdf", 13, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 9.", false, false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Chemistry Essentials", 43, null },
                    { 105, "https://cdn.example.com/files/chemistry-1-10.pdf", 13, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 10.", false, false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 10: Chemistry Essentials", 43, "https://cdn.example.com/videos/chemistry-1-10.mp4" },
                    { 106, "https://cdn.example.com/files/chemistry-2-1.pdf", 14, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 1.", false, true, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Chemistry Essentials", 44, "https://cdn.example.com/videos/chemistry-2-1.mp4" },
                    { 107, "https://cdn.example.com/files/chemistry-2-2.pdf", 14, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 2.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Chemistry Essentials", 44, "https://cdn.example.com/videos/chemistry-2-2.mp4" },
                    { 108, "https://cdn.example.com/files/chemistry-2-3.pdf", 14, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 3.", false, false, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Chemistry Essentials", 45, "https://cdn.example.com/videos/chemistry-2-3.mp4" },
                    { 109, null, 14, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 4.", false, false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Chemistry Essentials", 45, "https://cdn.example.com/videos/chemistry-2-4.mp4" },
                    { 110, "https://cdn.example.com/files/chemistry-2-5.pdf", 14, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 5.", false, false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Chemistry Essentials", 46, "https://cdn.example.com/videos/chemistry-2-5.mp4" },
                    { 111, null, 14, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 6.", false, false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Chemistry Essentials", 46, "https://cdn.example.com/videos/chemistry-2-6.mp4" },
                    { 112, null, 15, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 1.", false, true, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Chemistry Essentials", 47, "https://cdn.example.com/videos/chemistry-3-1.mp4" },
                    { 113, null, 15, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 2.", false, false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Chemistry Essentials", 47, null },
                    { 114, "https://cdn.example.com/files/chemistry-3-3.pdf", 15, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 3.", false, false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Chemistry Essentials", 47, null },
                    { 115, "https://cdn.example.com/files/chemistry-3-4.pdf", 15, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 4.", false, false, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Chemistry Essentials", 48, null },
                    { 116, null, 15, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 5.", false, false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Chemistry Essentials", 48, null },
                    { 117, null, 15, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 6.", false, false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Chemistry Essentials", 49, "https://cdn.example.com/videos/chemistry-3-6.mp4" },
                    { 118, null, 15, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 7.", false, false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Chemistry Essentials", 49, null },
                    { 119, "https://cdn.example.com/files/chemistry-3-8.pdf", 15, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 8.", false, false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Chemistry Essentials", 50, null },
                    { 120, "https://cdn.example.com/files/chemistry-3-9.pdf", 15, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 9.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Chemistry Essentials", 50, "https://cdn.example.com/videos/chemistry-3-9.mp4" },
                    { 121, "https://cdn.example.com/files/chemistry-3-10.pdf", 15, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 10.", false, false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 10: Chemistry Essentials", 50, null },
                    { 122, null, 16, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 1.", false, true, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Chemistry Essentials", 51, null },
                    { 123, "https://cdn.example.com/files/chemistry-4-2.pdf", 16, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 2.", false, false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Chemistry Essentials", 51, "https://cdn.example.com/videos/chemistry-4-2.mp4" },
                    { 124, null, 16, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 3.", false, false, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Chemistry Essentials", 52, "https://cdn.example.com/videos/chemistry-4-3.mp4" },
                    { 125, null, 16, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 4.", false, false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Chemistry Essentials", 52, null },
                    { 126, null, 16, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 5.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Chemistry Essentials", 52, null },
                    { 127, null, 16, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 6.", false, false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Chemistry Essentials", 53, "https://cdn.example.com/videos/chemistry-4-6.mp4" },
                    { 128, "https://cdn.example.com/files/chemistry-4-7.pdf", 16, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 7.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Chemistry Essentials", 53, null },
                    { 129, null, 16, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 8.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Chemistry Essentials", 54, "https://cdn.example.com/videos/chemistry-4-8.mp4" },
                    { 130, null, 16, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 9.", false, false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Chemistry Essentials", 54, "https://cdn.example.com/videos/chemistry-4-9.mp4" },
                    { 131, null, 16, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 10.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 10: Chemistry Essentials", 54, "https://cdn.example.com/videos/chemistry-4-10.mp4" },
                    { 132, "https://cdn.example.com/files/chemistry-5-1.pdf", 17, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 1.", false, true, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Chemistry Essentials", 55, "https://cdn.example.com/videos/chemistry-5-1.mp4" },
                    { 133, "https://cdn.example.com/files/chemistry-5-2.pdf", 17, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 2.", false, false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Chemistry Essentials", 55, "https://cdn.example.com/videos/chemistry-5-2.mp4" },
                    { 134, "https://cdn.example.com/files/chemistry-5-3.pdf", 17, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 3.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Chemistry Essentials", 55, "https://cdn.example.com/videos/chemistry-5-3.mp4" },
                    { 135, "https://cdn.example.com/files/chemistry-5-4.pdf", 17, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 4.", false, false, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Chemistry Essentials", 56, null },
                    { 136, "https://cdn.example.com/files/chemistry-5-5.pdf", 17, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 5.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Chemistry Essentials", 56, "https://cdn.example.com/videos/chemistry-5-5.mp4" },
                    { 137, "https://cdn.example.com/files/chemistry-5-6.pdf", 17, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 6.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Chemistry Essentials", 57, "https://cdn.example.com/videos/chemistry-5-6.mp4" },
                    { 138, "https://cdn.example.com/files/chemistry-5-7.pdf", 17, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 7.", false, false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Chemistry Essentials", 57, null },
                    { 139, "https://cdn.example.com/files/chemistry-5-8.pdf", 17, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for chemistry lesson 8.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Chemistry Essentials", 57, "https://cdn.example.com/videos/chemistry-5-8.mp4" },
                    { 140, "https://cdn.example.com/files/biology-1-1.pdf", 18, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 1.", false, true, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Biology Essentials", 58, null },
                    { 141, null, 18, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 2.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Biology Essentials", 58, null },
                    { 142, null, 18, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 3.", false, false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Biology Essentials", 58, "https://cdn.example.com/videos/biology-1-3.mp4" },
                    { 143, "https://cdn.example.com/files/biology-1-4.pdf", 18, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 4.", false, false, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Biology Essentials", 59, "https://cdn.example.com/videos/biology-1-4.mp4" },
                    { 144, "https://cdn.example.com/files/biology-1-5.pdf", 18, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 5.", false, false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Biology Essentials", 59, null },
                    { 145, "https://cdn.example.com/files/biology-1-6.pdf", 18, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 6.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: Biology Essentials", 59, null },
                    { 146, "https://cdn.example.com/files/biology-1-7.pdf", 18, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 7.", false, false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Biology Essentials", 60, null },
                    { 147, "https://cdn.example.com/files/biology-1-8.pdf", 18, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 8.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Biology Essentials", 60, null },
                    { 148, null, 18, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 9.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 9: Biology Essentials", 61, "https://cdn.example.com/videos/biology-1-9.mp4" },
                    { 149, "https://cdn.example.com/files/biology-1-10.pdf", 18, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 10.", false, false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 10: Biology Essentials", 61, "https://cdn.example.com/videos/biology-1-10.mp4" },
                    { 150, null, 18, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 11.", false, false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 11: Biology Essentials", 61, null },
                    { 151, "https://cdn.example.com/files/biology-2-1.pdf", 19, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 1.", false, true, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Biology Essentials", 62, null },
                    { 152, "https://cdn.example.com/files/biology-2-2.pdf", 19, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 2.", false, false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Biology Essentials", 62, "https://cdn.example.com/videos/biology-2-2.mp4" },
                    { 153, null, 19, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 3.", false, false, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Biology Essentials", 63, "https://cdn.example.com/videos/biology-2-3.mp4" },
                    { 154, "https://cdn.example.com/files/biology-2-4.pdf", 19, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 4.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Biology Essentials", 63, "https://cdn.example.com/videos/biology-2-4.mp4" },
                    { 155, "https://cdn.example.com/files/biology-2-5.pdf", 19, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 5.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Biology Essentials", 64, null },
                    { 156, "https://cdn.example.com/files/biology-2-6.pdf", 19, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 6.", false, false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Biology Essentials", 64, "https://cdn.example.com/videos/biology-2-6.mp4" },
                    { 157, null, 20, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 1.", false, true, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Biology Essentials", 65, "https://cdn.example.com/videos/biology-3-1.mp4" },
                    { 158, null, 20, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 2.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Biology Essentials", 65, "https://cdn.example.com/videos/biology-3-2.mp4" },
                    { 159, "https://cdn.example.com/files/biology-3-3.pdf", 20, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 3.", false, false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Biology Essentials", 65, "https://cdn.example.com/videos/biology-3-3.mp4" },
                    { 160, "https://cdn.example.com/files/biology-3-4.pdf", 20, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 4.", false, false, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Biology Essentials", 66, null },
                    { 161, "https://cdn.example.com/files/biology-3-5.pdf", 20, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 5.", false, false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Biology Essentials", 66, null },
                    { 162, null, 20, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 6.", false, false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: Biology Essentials", 66, "https://cdn.example.com/videos/biology-3-6.mp4" },
                    { 163, "https://cdn.example.com/files/biology-3-7.pdf", 20, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 7.", false, false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Biology Essentials", 67, null },
                    { 164, null, 20, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 8.", false, false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Biology Essentials", 67, null },
                    { 165, null, 20, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 9.", false, false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 9: Biology Essentials", 67, null },
                    { 166, "https://cdn.example.com/files/biology-4-1.pdf", 21, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 1.", false, true, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Biology Essentials", 68, null },
                    { 167, null, 21, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 2.", false, false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Biology Essentials", 68, "https://cdn.example.com/videos/biology-4-2.mp4" },
                    { 168, "https://cdn.example.com/files/biology-4-3.pdf", 21, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 3.", false, false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Biology Essentials", 68, "https://cdn.example.com/videos/biology-4-3.mp4" },
                    { 169, null, 21, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 4.", false, false, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Biology Essentials", 69, null },
                    { 170, "https://cdn.example.com/files/biology-4-5.pdf", 21, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 5.", false, false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Biology Essentials", 69, null },
                    { 171, "https://cdn.example.com/files/biology-4-6.pdf", 21, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 6.", false, false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Biology Essentials", 70, null },
                    { 172, "https://cdn.example.com/files/biology-4-7.pdf", 21, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 7.", false, false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Biology Essentials", 70, null },
                    { 173, null, 22, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 1.", false, true, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Biology Essentials", 71, null },
                    { 174, "https://cdn.example.com/files/biology-5-2.pdf", 22, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 2.", false, false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Biology Essentials", 71, "https://cdn.example.com/videos/biology-5-2.mp4" },
                    { 175, "https://cdn.example.com/files/biology-5-3.pdf", 22, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 3.", false, false, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Biology Essentials", 72, null },
                    { 176, "https://cdn.example.com/files/biology-5-4.pdf", 22, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 4.", false, false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Biology Essentials", 72, null },
                    { 177, null, 22, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 5.", false, false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Biology Essentials", 73, "https://cdn.example.com/videos/biology-5-5.mp4" },
                    { 178, null, 22, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 6.", false, false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Biology Essentials", 73, "https://cdn.example.com/videos/biology-5-6.mp4" },
                    { 179, null, 22, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 7.", false, false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Biology Essentials", 74, "https://cdn.example.com/videos/biology-5-7.mp4" },
                    { 180, null, 22, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for biology lesson 8.", false, false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Biology Essentials", 74, "https://cdn.example.com/videos/biology-5-8.mp4" },
                    { 181, null, 23, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 1.", false, true, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Language Essentials", 75, null },
                    { 182, null, 23, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 2.", false, false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Language Essentials", 75, "https://cdn.example.com/videos/english-language-1-2.mp4" },
                    { 183, null, 23, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 3.", false, false, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: English Language Essentials", 76, null },
                    { 184, "https://cdn.example.com/files/english-language-1-4.pdf", 23, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 4.", false, false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: English Language Essentials", 76, null },
                    { 185, null, 23, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 5.", false, false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: English Language Essentials", 76, null },
                    { 186, "https://cdn.example.com/files/english-language-1-6.pdf", 23, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 6.", false, false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: English Language Essentials", 77, "https://cdn.example.com/videos/english-language-1-6.mp4" },
                    { 187, null, 23, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 7.", false, false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: English Language Essentials", 77, "https://cdn.example.com/videos/english-language-1-7.mp4" },
                    { 188, "https://cdn.example.com/files/english-language-1-8.pdf", 23, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 8.", false, false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: English Language Essentials", 77, "https://cdn.example.com/videos/english-language-1-8.mp4" },
                    { 189, null, 24, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 1.", false, true, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Language Essentials", 78, null },
                    { 190, null, 24, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 2.", false, false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Language Essentials", 78, null },
                    { 191, "https://cdn.example.com/files/english-language-2-3.pdf", 24, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 3.", false, false, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: English Language Essentials", 79, null },
                    { 192, null, 24, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 4.", false, false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: English Language Essentials", 79, "https://cdn.example.com/videos/english-language-2-4.mp4" },
                    { 193, "https://cdn.example.com/files/english-language-2-5.pdf", 24, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 5.", false, false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: English Language Essentials", 80, "https://cdn.example.com/videos/english-language-2-5.mp4" },
                    { 194, "https://cdn.example.com/files/english-language-2-6.pdf", 24, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 6.", false, false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: English Language Essentials", 80, "https://cdn.example.com/videos/english-language-2-6.mp4" },
                    { 195, "https://cdn.example.com/files/english-language-2-7.pdf", 24, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 7.", false, false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: English Language Essentials", 81, null },
                    { 196, "https://cdn.example.com/files/english-language-2-8.pdf", 24, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 8.", false, false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: English Language Essentials", 81, null },
                    { 197, "https://cdn.example.com/files/english-language-3-1.pdf", 25, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 1.", false, true, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Language Essentials", 82, null },
                    { 198, "https://cdn.example.com/files/english-language-3-2.pdf", 25, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 2.", false, false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Language Essentials", 82, null },
                    { 199, null, 25, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 3.", false, false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: English Language Essentials", 82, "https://cdn.example.com/videos/english-language-3-3.mp4" },
                    { 200, null, 25, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 4.", false, false, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: English Language Essentials", 83, null },
                    { 201, "https://cdn.example.com/files/english-language-3-5.pdf", 25, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 5.", false, false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: English Language Essentials", 83, "https://cdn.example.com/videos/english-language-3-5.mp4" },
                    { 202, "https://cdn.example.com/files/english-language-3-6.pdf", 25, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 6.", false, false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: English Language Essentials", 84, "https://cdn.example.com/videos/english-language-3-6.mp4" },
                    { 203, "https://cdn.example.com/files/english-language-3-7.pdf", 25, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 7.", false, false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: English Language Essentials", 84, null },
                    { 204, "https://cdn.example.com/files/english-language-3-8.pdf", 25, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 8.", false, false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: English Language Essentials", 85, null },
                    { 205, null, 25, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 9.", false, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: English Language Essentials", 85, null },
                    { 206, null, 26, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 1.", false, true, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Language Essentials", 86, null },
                    { 207, "https://cdn.example.com/files/english-language-4-2.pdf", 26, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 2.", false, false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Language Essentials", 86, "https://cdn.example.com/videos/english-language-4-2.mp4" },
                    { 208, null, 26, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 3.", false, false, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: English Language Essentials", 87, null },
                    { 209, null, 26, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 4.", false, false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: English Language Essentials", 87, "https://cdn.example.com/videos/english-language-4-4.mp4" },
                    { 210, null, 26, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 5.", false, false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: English Language Essentials", 88, null },
                    { 211, null, 26, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 6.", false, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: English Language Essentials", 88, null },
                    { 212, null, 26, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 7.", false, false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 7: English Language Essentials", 88, null },
                    { 213, null, 27, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 1.", false, true, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Language Essentials", 89, null },
                    { 214, null, 27, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 2.", false, false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Language Essentials", 89, null },
                    { 215, "https://cdn.example.com/files/english-language-5-3.pdf", 27, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 3.", false, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: English Language Essentials", 89, null },
                    { 216, "https://cdn.example.com/files/english-language-5-4.pdf", 27, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 4.", false, false, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: English Language Essentials", 90, "https://cdn.example.com/videos/english-language-5-4.mp4" },
                    { 217, null, 27, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 5.", false, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: English Language Essentials", 90, null },
                    { 218, "https://cdn.example.com/files/english-language-5-6.pdf", 27, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 6.", false, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: English Language Essentials", 91, null },
                    { 219, null, 27, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 7.", false, false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: English Language Essentials", 91, "https://cdn.example.com/videos/english-language-5-7.mp4" },
                    { 220, "https://cdn.example.com/files/english-language-5-8.pdf", 27, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 8.", false, false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: English Language Essentials", 91, "https://cdn.example.com/videos/english-language-5-8.mp4" },
                    { 221, "https://cdn.example.com/files/english-language-5-9.pdf", 27, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 9.", false, false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 9: English Language Essentials", 92, null },
                    { 222, null, 27, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 10.", false, false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 10: English Language Essentials", 92, "https://cdn.example.com/videos/english-language-5-10.mp4" },
                    { 223, null, 27, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english language lesson 11.", false, false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 11: English Language Essentials", 92, null },
                    { 224, "https://cdn.example.com/files/english-literature-1-1.pdf", 28, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 1.", false, true, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Literature Essentials", 93, "https://cdn.example.com/videos/english-literature-1-1.mp4" },
                    { 225, null, 28, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 2.", false, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Literature Essentials", 93, "https://cdn.example.com/videos/english-literature-1-2.mp4" },
                    { 226, "https://cdn.example.com/files/english-literature-1-3.pdf", 28, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 3.", false, false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: English Literature Essentials", 93, null },
                    { 227, "https://cdn.example.com/files/english-literature-1-4.pdf", 28, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 4.", false, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: English Literature Essentials", 94, "https://cdn.example.com/videos/english-literature-1-4.mp4" },
                    { 228, null, 28, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 5.", false, false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: English Literature Essentials", 94, "https://cdn.example.com/videos/english-literature-1-5.mp4" },
                    { 229, null, 28, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 6.", false, false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: English Literature Essentials", 94, "https://cdn.example.com/videos/english-literature-1-6.mp4" },
                    { 230, null, 28, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 7.", false, false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: English Literature Essentials", 95, "https://cdn.example.com/videos/english-literature-1-7.mp4" },
                    { 231, "https://cdn.example.com/files/english-literature-1-8.pdf", 28, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 8.", false, false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: English Literature Essentials", 95, null },
                    { 232, "https://cdn.example.com/files/english-literature-1-9.pdf", 28, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 9.", false, false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 9: English Literature Essentials", 95, "https://cdn.example.com/videos/english-literature-1-9.mp4" },
                    { 233, "https://cdn.example.com/files/english-literature-2-1.pdf", 29, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 1.", false, true, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Literature Essentials", 96, null },
                    { 234, "https://cdn.example.com/files/english-literature-2-2.pdf", 29, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 2.", false, false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Literature Essentials", 96, "https://cdn.example.com/videos/english-literature-2-2.mp4" },
                    { 235, "https://cdn.example.com/files/english-literature-2-3.pdf", 29, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 3.", false, false, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: English Literature Essentials", 97, "https://cdn.example.com/videos/english-literature-2-3.mp4" },
                    { 236, null, 29, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 4.", false, false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: English Literature Essentials", 97, "https://cdn.example.com/videos/english-literature-2-4.mp4" },
                    { 237, null, 29, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 5.", false, false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: English Literature Essentials", 97, null },
                    { 238, "https://cdn.example.com/files/english-literature-2-6.pdf", 29, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 6.", false, false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: English Literature Essentials", 98, "https://cdn.example.com/videos/english-literature-2-6.mp4" },
                    { 239, "https://cdn.example.com/files/english-literature-2-7.pdf", 29, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 7.", false, false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: English Literature Essentials", 98, "https://cdn.example.com/videos/english-literature-2-7.mp4" },
                    { 240, "https://cdn.example.com/files/english-literature-3-1.pdf", 30, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 1.", false, true, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Literature Essentials", 99, "https://cdn.example.com/videos/english-literature-3-1.mp4" },
                    { 241, null, 30, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 2.", false, false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Literature Essentials", 99, null },
                    { 242, "https://cdn.example.com/files/english-literature-3-3.pdf", 30, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 3.", false, false, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: English Literature Essentials", 100, null },
                    { 243, null, 30, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 4.", false, false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: English Literature Essentials", 100, null },
                    { 244, null, 30, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 5.", false, false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: English Literature Essentials", 101, "https://cdn.example.com/videos/english-literature-3-5.mp4" },
                    { 245, null, 30, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 6.", false, false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: English Literature Essentials", 101, null },
                    { 246, "https://cdn.example.com/files/english-literature-3-7.pdf", 30, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 7.", false, false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: English Literature Essentials", 102, "https://cdn.example.com/videos/english-literature-3-7.mp4" },
                    { 247, "https://cdn.example.com/files/english-literature-3-8.pdf", 30, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 8.", false, false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: English Literature Essentials", 102, "https://cdn.example.com/videos/english-literature-3-8.mp4" },
                    { 248, "https://cdn.example.com/files/english-literature-3-9.pdf", 30, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 9.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 9: English Literature Essentials", 102, "https://cdn.example.com/videos/english-literature-3-9.mp4" },
                    { 249, null, 31, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 1.", false, true, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Literature Essentials", 103, null },
                    { 250, "https://cdn.example.com/files/english-literature-4-2.pdf", 31, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 2.", false, false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Literature Essentials", 103, null },
                    { 251, "https://cdn.example.com/files/english-literature-4-3.pdf", 31, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 3.", false, false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: English Literature Essentials", 103, null },
                    { 252, null, 31, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 4.", false, false, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: English Literature Essentials", 104, null },
                    { 253, "https://cdn.example.com/files/english-literature-4-5.pdf", 31, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 5.", false, false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: English Literature Essentials", 104, null },
                    { 254, "https://cdn.example.com/files/english-literature-4-6.pdf", 31, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 6.", false, false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: English Literature Essentials", 104, "https://cdn.example.com/videos/english-literature-4-6.mp4" },
                    { 255, null, 31, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 7.", false, false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: English Literature Essentials", 105, "https://cdn.example.com/videos/english-literature-4-7.mp4" },
                    { 256, "https://cdn.example.com/files/english-literature-4-8.pdf", 31, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 8.", false, false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: English Literature Essentials", 105, null },
                    { 257, "https://cdn.example.com/files/english-literature-4-9.pdf", 31, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 9.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 9: English Literature Essentials", 105, "https://cdn.example.com/videos/english-literature-4-9.mp4" },
                    { 258, null, 32, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 1.", false, true, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: English Literature Essentials", 106, null },
                    { 259, null, 32, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 2.", false, false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: English Literature Essentials", 106, "https://cdn.example.com/videos/english-literature-5-2.mp4" },
                    { 260, "https://cdn.example.com/files/english-literature-5-3.pdf", 32, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 3.", false, false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: English Literature Essentials", 106, null },
                    { 261, "https://cdn.example.com/files/english-literature-5-4.pdf", 32, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 4.", false, false, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: English Literature Essentials", 107, null },
                    { 262, "https://cdn.example.com/files/english-literature-5-5.pdf", 32, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 5.", false, false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: English Literature Essentials", 107, null },
                    { 263, "https://cdn.example.com/files/english-literature-5-6.pdf", 32, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 6.", false, false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: English Literature Essentials", 108, "https://cdn.example.com/videos/english-literature-5-6.mp4" },
                    { 264, "https://cdn.example.com/files/english-literature-5-7.pdf", 32, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 7.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: English Literature Essentials", 108, null },
                    { 265, null, 32, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 8.", false, false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: English Literature Essentials", 108, null },
                    { 266, "https://cdn.example.com/files/english-literature-5-9.pdf", 32, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 9.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 9: English Literature Essentials", 109, null },
                    { 267, null, 32, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 10.", false, false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 10: English Literature Essentials", 109, "https://cdn.example.com/videos/english-literature-5-10.mp4" },
                    { 268, null, 32, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for english literature lesson 11.", false, false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 11: English Literature Essentials", 109, "https://cdn.example.com/videos/english-literature-5-11.mp4" },
                    { 269, null, 33, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 1.", false, true, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: History Essentials", 110, null },
                    { 270, "https://cdn.example.com/files/history-1-2.pdf", 33, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 2.", false, false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: History Essentials", 110, "https://cdn.example.com/videos/history-1-2.mp4" },
                    { 271, "https://cdn.example.com/files/history-1-3.pdf", 33, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 3.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: History Essentials", 110, null },
                    { 272, null, 33, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 4.", false, false, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: History Essentials", 111, "https://cdn.example.com/videos/history-1-4.mp4" },
                    { 273, null, 33, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 5.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: History Essentials", 111, null },
                    { 274, null, 33, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 6.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: History Essentials", 112, null },
                    { 275, null, 33, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 7.", false, false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: History Essentials", 112, null },
                    { 276, null, 33, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 8.", false, false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: History Essentials", 112, null },
                    { 277, "https://cdn.example.com/files/history-2-1.pdf", 34, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 1.", false, true, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: History Essentials", 113, null },
                    { 278, null, 34, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 2.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: History Essentials", 113, null },
                    { 279, null, 34, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 3.", false, false, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: History Essentials", 114, null },
                    { 280, "https://cdn.example.com/files/history-2-4.pdf", 34, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 4.", false, false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: History Essentials", 114, null },
                    { 281, "https://cdn.example.com/files/history-2-5.pdf", 34, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 5.", false, false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: History Essentials", 115, "https://cdn.example.com/videos/history-2-5.mp4" },
                    { 282, "https://cdn.example.com/files/history-2-6.pdf", 34, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 6.", false, false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: History Essentials", 115, "https://cdn.example.com/videos/history-2-6.mp4" },
                    { 283, null, 34, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 7.", false, false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: History Essentials", 116, null },
                    { 284, null, 34, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 8.", false, false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: History Essentials", 116, null },
                    { 285, null, 35, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 1.", false, true, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: History Essentials", 117, "https://cdn.example.com/videos/history-3-1.mp4" },
                    { 286, "https://cdn.example.com/files/history-3-2.pdf", 35, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 2.", false, false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: History Essentials", 117, "https://cdn.example.com/videos/history-3-2.mp4" },
                    { 287, null, 35, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 3.", false, false, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: History Essentials", 118, "https://cdn.example.com/videos/history-3-3.mp4" },
                    { 288, "https://cdn.example.com/files/history-3-4.pdf", 35, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 4.", false, false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: History Essentials", 118, "https://cdn.example.com/videos/history-3-4.mp4" },
                    { 289, "https://cdn.example.com/files/history-3-5.pdf", 35, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 5.", false, false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: History Essentials", 118, "https://cdn.example.com/videos/history-3-5.mp4" },
                    { 290, "https://cdn.example.com/files/history-3-6.pdf", 35, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 6.", false, false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: History Essentials", 119, null },
                    { 291, "https://cdn.example.com/files/history-3-7.pdf", 35, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 7.", false, false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: History Essentials", 119, null },
                    { 292, null, 35, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 8.", false, false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: History Essentials", 119, "https://cdn.example.com/videos/history-3-8.mp4" },
                    { 293, "https://cdn.example.com/files/history-4-1.pdf", 36, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 1.", false, true, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: History Essentials", 120, null },
                    { 294, "https://cdn.example.com/files/history-4-2.pdf", 36, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 2.", false, false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: History Essentials", 120, null },
                    { 295, null, 36, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 3.", false, false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: History Essentials", 120, null },
                    { 296, "https://cdn.example.com/files/history-4-4.pdf", 36, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 4.", false, false, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: History Essentials", 121, null },
                    { 297, null, 36, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 5.", false, false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: History Essentials", 121, "https://cdn.example.com/videos/history-4-5.mp4" },
                    { 298, null, 36, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 6.", false, false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: History Essentials", 121, "https://cdn.example.com/videos/history-4-6.mp4" },
                    { 299, "https://cdn.example.com/files/history-4-7.pdf", 36, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 7.", false, false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: History Essentials", 122, "https://cdn.example.com/videos/history-4-7.mp4" },
                    { 300, "https://cdn.example.com/files/history-4-8.pdf", 36, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 8.", false, false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: History Essentials", 122, null },
                    { 301, "https://cdn.example.com/files/history-4-9.pdf", 36, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 9.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 9: History Essentials", 122, null },
                    { 302, null, 36, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 10.", false, false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 10: History Essentials", 123, null },
                    { 303, "https://cdn.example.com/files/history-4-11.pdf", 36, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 11.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 11: History Essentials", 123, "https://cdn.example.com/videos/history-4-11.mp4" },
                    { 304, "https://cdn.example.com/files/history-5-1.pdf", 37, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 1.", false, true, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: History Essentials", 124, null },
                    { 305, null, 37, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 2.", false, false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: History Essentials", 124, null },
                    { 306, null, 37, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 3.", false, false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: History Essentials", 125, "https://cdn.example.com/videos/history-5-3.mp4" },
                    { 307, null, 37, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 4.", false, false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: History Essentials", 125, null },
                    { 308, "https://cdn.example.com/files/history-5-5.pdf", 37, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 5.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: History Essentials", 125, null },
                    { 309, null, 37, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 6.", false, false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: History Essentials", 126, null },
                    { 310, null, 37, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 7.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: History Essentials", 126, "https://cdn.example.com/videos/history-5-7.mp4" },
                    { 311, "https://cdn.example.com/files/history-5-8.pdf", 37, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 8.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: History Essentials", 126, null },
                    { 312, "https://cdn.example.com/files/history-5-9.pdf", 37, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 9.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 9: History Essentials", 127, null },
                    { 313, null, 37, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 10.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 10: History Essentials", 127, null },
                    { 314, "https://cdn.example.com/files/history-5-11.pdf", 37, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for history lesson 11.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 11: History Essentials", 127, "https://cdn.example.com/videos/history-5-11.mp4" },
                    { 315, "https://cdn.example.com/files/geography-1-1.pdf", 38, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 1.", false, true, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Geography Essentials", 128, null },
                    { 316, "https://cdn.example.com/files/geography-1-2.pdf", 38, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 2.", false, false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Geography Essentials", 128, null },
                    { 317, null, 38, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 3.", false, false, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Geography Essentials", 129, null },
                    { 318, "https://cdn.example.com/files/geography-1-4.pdf", 38, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 4.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Geography Essentials", 129, null },
                    { 319, "https://cdn.example.com/files/geography-1-5.pdf", 38, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 5.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Geography Essentials", 129, null },
                    { 320, "https://cdn.example.com/files/geography-1-6.pdf", 38, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 6.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Geography Essentials", 130, null },
                    { 321, "https://cdn.example.com/files/geography-1-7.pdf", 38, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 7.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Geography Essentials", 130, "https://cdn.example.com/videos/geography-1-7.mp4" },
                    { 322, null, 38, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 8.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Geography Essentials", 131, null },
                    { 323, null, 38, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 9.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Geography Essentials", 131, null },
                    { 324, "https://cdn.example.com/files/geography-1-10.pdf", 38, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 10.", false, false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 10: Geography Essentials", 131, "https://cdn.example.com/videos/geography-1-10.mp4" },
                    { 325, "https://cdn.example.com/files/geography-2-1.pdf", 39, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 1.", false, true, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Geography Essentials", 132, "https://cdn.example.com/videos/geography-2-1.mp4" },
                    { 326, null, 39, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 2.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Geography Essentials", 132, null },
                    { 327, "https://cdn.example.com/files/geography-2-3.pdf", 39, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 3.", false, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Geography Essentials", 133, null },
                    { 328, "https://cdn.example.com/files/geography-2-4.pdf", 39, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 4.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Geography Essentials", 133, "https://cdn.example.com/videos/geography-2-4.mp4" },
                    { 329, "https://cdn.example.com/files/geography-2-5.pdf", 39, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 5.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Geography Essentials", 133, null },
                    { 330, "https://cdn.example.com/files/geography-2-6.pdf", 39, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 6.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Geography Essentials", 134, "https://cdn.example.com/videos/geography-2-6.mp4" },
                    { 331, "https://cdn.example.com/files/geography-2-7.pdf", 39, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 7.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Geography Essentials", 134, "https://cdn.example.com/videos/geography-2-7.mp4" },
                    { 332, null, 39, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 8.", false, false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Geography Essentials", 134, null },
                    { 333, "https://cdn.example.com/files/geography-3-1.pdf", 40, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 1.", false, true, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Geography Essentials", 135, "https://cdn.example.com/videos/geography-3-1.mp4" },
                    { 334, null, 40, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 2.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Geography Essentials", 135, "https://cdn.example.com/videos/geography-3-2.mp4" },
                    { 335, "https://cdn.example.com/files/geography-3-3.pdf", 40, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 3.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Geography Essentials", 135, null },
                    { 336, null, 40, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 4.", false, false, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Geography Essentials", 136, null },
                    { 337, "https://cdn.example.com/files/geography-3-5.pdf", 40, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 5.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Geography Essentials", 136, "https://cdn.example.com/videos/geography-3-5.mp4" },
                    { 338, null, 40, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 6.", false, false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: Geography Essentials", 136, "https://cdn.example.com/videos/geography-3-6.mp4" },
                    { 339, "https://cdn.example.com/files/geography-3-7.pdf", 40, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 7.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Geography Essentials", 137, "https://cdn.example.com/videos/geography-3-7.mp4" },
                    { 340, "https://cdn.example.com/files/geography-3-8.pdf", 40, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 8.", false, false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Geography Essentials", 137, "https://cdn.example.com/videos/geography-3-8.mp4" },
                    { 341, "https://cdn.example.com/files/geography-3-9.pdf", 40, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 9.", false, false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 9: Geography Essentials", 137, "https://cdn.example.com/videos/geography-3-9.mp4" },
                    { 342, "https://cdn.example.com/files/geography-4-1.pdf", 41, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 1.", false, true, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Geography Essentials", 138, "https://cdn.example.com/videos/geography-4-1.mp4" },
                    { 343, null, 41, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 2.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Geography Essentials", 138, null },
                    { 344, "https://cdn.example.com/files/geography-4-3.pdf", 41, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 3.", false, false, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Geography Essentials", 139, null },
                    { 345, "https://cdn.example.com/files/geography-4-4.pdf", 41, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 4.", false, false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Geography Essentials", 139, "https://cdn.example.com/videos/geography-4-4.mp4" },
                    { 346, null, 41, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 5.", false, false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Geography Essentials", 140, "https://cdn.example.com/videos/geography-4-5.mp4" },
                    { 347, null, 41, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 6.", false, false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Geography Essentials", 140, "https://cdn.example.com/videos/geography-4-6.mp4" },
                    { 348, null, 42, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 1.", false, true, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Geography Essentials", 141, "https://cdn.example.com/videos/geography-5-1.mp4" },
                    { 349, null, 42, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 2.", false, false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Geography Essentials", 141, "https://cdn.example.com/videos/geography-5-2.mp4" },
                    { 350, "https://cdn.example.com/files/geography-5-3.pdf", 42, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 3.", false, false, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Geography Essentials", 142, "https://cdn.example.com/videos/geography-5-3.mp4" },
                    { 351, null, 42, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 4.", false, false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Geography Essentials", 142, null },
                    { 352, null, 42, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 5.", false, false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Geography Essentials", 143, "https://cdn.example.com/videos/geography-5-5.mp4" },
                    { 353, "https://cdn.example.com/files/geography-5-6.pdf", 42, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 6.", false, false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Geography Essentials", 143, "https://cdn.example.com/videos/geography-5-6.mp4" },
                    { 354, "https://cdn.example.com/files/geography-5-7.pdf", 42, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 7.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 7: Geography Essentials", 143, "https://cdn.example.com/videos/geography-5-7.mp4" },
                    { 355, "https://cdn.example.com/files/geography-5-8.pdf", 42, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 8.", false, false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Geography Essentials", 144, null },
                    { 356, "https://cdn.example.com/files/geography-5-9.pdf", 42, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for geography lesson 9.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Geography Essentials", 144, "https://cdn.example.com/videos/geography-5-9.mp4" },
                    { 357, "https://cdn.example.com/files/economics-1-1.pdf", 43, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 1.", false, true, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Economics Essentials", 145, null },
                    { 358, "https://cdn.example.com/files/economics-1-2.pdf", 43, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 2.", false, false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Economics Essentials", 145, "https://cdn.example.com/videos/economics-1-2.mp4" },
                    { 359, null, 43, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 3.", false, false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Economics Essentials", 145, "https://cdn.example.com/videos/economics-1-3.mp4" },
                    { 360, "https://cdn.example.com/files/economics-1-4.pdf", 43, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 4.", false, false, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Economics Essentials", 146, null },
                    { 361, null, 43, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 5.", false, false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Economics Essentials", 146, "https://cdn.example.com/videos/economics-1-5.mp4" },
                    { 362, null, 43, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 6.", false, false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Economics Essentials", 147, null },
                    { 363, null, 43, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 7.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Economics Essentials", 147, "https://cdn.example.com/videos/economics-1-7.mp4" },
                    { 364, "https://cdn.example.com/files/economics-1-8.pdf", 43, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 8.", false, false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Economics Essentials", 147, null },
                    { 365, "https://cdn.example.com/files/economics-1-9.pdf", 43, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 9.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 9: Economics Essentials", 148, "https://cdn.example.com/videos/economics-1-9.mp4" },
                    { 366, null, 43, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 10.", false, false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 10: Economics Essentials", 148, "https://cdn.example.com/videos/economics-1-10.mp4" },
                    { 367, "https://cdn.example.com/files/economics-1-11.pdf", 43, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 11.", false, false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 11: Economics Essentials", 148, null },
                    { 368, "https://cdn.example.com/files/economics-2-1.pdf", 44, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 1.", false, true, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Economics Essentials", 149, null },
                    { 369, null, 44, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 2.", false, false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Economics Essentials", 149, "https://cdn.example.com/videos/economics-2-2.mp4" },
                    { 370, "https://cdn.example.com/files/economics-2-3.pdf", 44, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 3.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Economics Essentials", 149, "https://cdn.example.com/videos/economics-2-3.mp4" },
                    { 371, "https://cdn.example.com/files/economics-2-4.pdf", 44, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 4.", false, false, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Economics Essentials", 150, "https://cdn.example.com/videos/economics-2-4.mp4" },
                    { 372, "https://cdn.example.com/files/economics-2-5.pdf", 44, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 5.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Economics Essentials", 150, "https://cdn.example.com/videos/economics-2-5.mp4" },
                    { 373, null, 44, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 6.", false, false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: Economics Essentials", 150, null },
                    { 374, null, 44, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 7.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Economics Essentials", 151, "https://cdn.example.com/videos/economics-2-7.mp4" },
                    { 375, null, 44, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 8.", false, false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Economics Essentials", 151, "https://cdn.example.com/videos/economics-2-8.mp4" },
                    { 376, null, 45, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 1.", false, true, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Economics Essentials", 152, null },
                    { 377, null, 45, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 2.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Economics Essentials", 152, null },
                    { 378, null, 45, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 3.", false, false, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Economics Essentials", 153, "https://cdn.example.com/videos/economics-3-3.mp4" },
                    { 379, null, 45, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 4.", false, false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Economics Essentials", 153, null },
                    { 380, null, 45, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 5.", false, false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Economics Essentials", 153, "https://cdn.example.com/videos/economics-3-5.mp4" },
                    { 381, null, 45, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 6.", false, false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Economics Essentials", 154, "https://cdn.example.com/videos/economics-3-6.mp4" },
                    { 382, "https://cdn.example.com/files/economics-3-7.pdf", 45, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 7.", false, false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Economics Essentials", 154, "https://cdn.example.com/videos/economics-3-7.mp4" },
                    { 383, "https://cdn.example.com/files/economics-4-1.pdf", 46, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 1.", false, true, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Economics Essentials", 155, null },
                    { 384, "https://cdn.example.com/files/economics-4-2.pdf", 46, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 2.", false, false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Economics Essentials", 155, null },
                    { 385, "https://cdn.example.com/files/economics-4-3.pdf", 46, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 3.", false, false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Economics Essentials", 155, null },
                    { 386, "https://cdn.example.com/files/economics-4-4.pdf", 46, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 4.", false, false, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Economics Essentials", 156, null },
                    { 387, null, 46, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 5.", false, false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Economics Essentials", 156, "https://cdn.example.com/videos/economics-4-5.mp4" },
                    { 388, null, 46, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 6.", false, false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Economics Essentials", 157, null },
                    { 389, "https://cdn.example.com/files/economics-4-7.pdf", 46, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 7.", false, false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Economics Essentials", 157, null },
                    { 390, "https://cdn.example.com/files/economics-5-1.pdf", 47, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 1.", false, true, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Economics Essentials", 158, null },
                    { 391, null, 47, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 2.", false, false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Economics Essentials", 158, null },
                    { 392, null, 47, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 3.", false, false, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Economics Essentials", 159, null },
                    { 393, "https://cdn.example.com/files/economics-5-4.pdf", 47, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 4.", false, false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Economics Essentials", 159, null },
                    { 394, null, 47, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 5.", false, false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Economics Essentials", 160, "https://cdn.example.com/videos/economics-5-5.mp4" },
                    { 395, "https://cdn.example.com/files/economics-5-6.pdf", 47, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 6.", false, false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Economics Essentials", 160, null },
                    { 396, "https://cdn.example.com/files/economics-5-7.pdf", 47, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 7.", false, false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Economics Essentials", 161, "https://cdn.example.com/videos/economics-5-7.mp4" },
                    { 397, "https://cdn.example.com/files/economics-5-8.pdf", 47, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for economics lesson 8.", false, false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Economics Essentials", 161, null },
                    { 398, "https://cdn.example.com/files/business-studies-1-1.pdf", 48, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 1.", false, true, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Business Studies Essentials", 162, "https://cdn.example.com/videos/business-studies-1-1.mp4" },
                    { 399, null, 48, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 2.", false, false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Business Studies Essentials", 162, null },
                    { 400, null, 48, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 3.", false, false, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Business Studies Essentials", 163, "https://cdn.example.com/videos/business-studies-1-3.mp4" },
                    { 401, null, 48, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 4.", false, false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Business Studies Essentials", 163, null },
                    { 402, null, 48, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 5.", false, false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Business Studies Essentials", 164, null },
                    { 403, null, 48, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 6.", false, false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Business Studies Essentials", 164, "https://cdn.example.com/videos/business-studies-1-6.mp4" },
                    { 404, null, 48, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 7.", false, false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 7: Business Studies Essentials", 164, "https://cdn.example.com/videos/business-studies-1-7.mp4" },
                    { 405, "https://cdn.example.com/files/business-studies-2-1.pdf", 49, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 1.", false, true, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Business Studies Essentials", 165, null },
                    { 406, null, 49, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 2.", false, false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Business Studies Essentials", 165, "https://cdn.example.com/videos/business-studies-2-2.mp4" },
                    { 407, "https://cdn.example.com/files/business-studies-2-3.pdf", 49, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 3.", false, false, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Business Studies Essentials", 166, null },
                    { 408, "https://cdn.example.com/files/business-studies-2-4.pdf", 49, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 4.", false, false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Business Studies Essentials", 166, null },
                    { 409, null, 49, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 5.", false, false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Business Studies Essentials", 167, null },
                    { 410, null, 49, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 6.", false, false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Business Studies Essentials", 167, "https://cdn.example.com/videos/business-studies-2-6.mp4" },
                    { 411, "https://cdn.example.com/files/business-studies-2-7.pdf", 49, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 7.", false, false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Business Studies Essentials", 168, "https://cdn.example.com/videos/business-studies-2-7.mp4" },
                    { 412, null, 49, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 8.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Business Studies Essentials", 168, "https://cdn.example.com/videos/business-studies-2-8.mp4" },
                    { 413, null, 50, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 1.", false, true, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Business Studies Essentials", 169, "https://cdn.example.com/videos/business-studies-3-1.mp4" },
                    { 414, null, 50, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 2.", false, false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Business Studies Essentials", 169, "https://cdn.example.com/videos/business-studies-3-2.mp4" },
                    { 415, "https://cdn.example.com/files/business-studies-3-3.pdf", 50, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 3.", false, false, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Business Studies Essentials", 170, null },
                    { 416, null, 50, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 4.", false, false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Business Studies Essentials", 170, null },
                    { 417, "https://cdn.example.com/files/business-studies-3-5.pdf", 50, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 5.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Business Studies Essentials", 170, "https://cdn.example.com/videos/business-studies-3-5.mp4" },
                    { 418, "https://cdn.example.com/files/business-studies-3-6.pdf", 50, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 6.", false, false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Business Studies Essentials", 171, "https://cdn.example.com/videos/business-studies-3-6.mp4" },
                    { 419, "https://cdn.example.com/files/business-studies-3-7.pdf", 50, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 7.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Business Studies Essentials", 171, null },
                    { 420, null, 50, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 8.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Business Studies Essentials", 171, null },
                    { 421, null, 50, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 9.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 9: Business Studies Essentials", 172, null },
                    { 422, "https://cdn.example.com/files/business-studies-3-10.pdf", 50, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 10.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 10: Business Studies Essentials", 172, "https://cdn.example.com/videos/business-studies-3-10.mp4" },
                    { 423, null, 50, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 11.", false, false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 11: Business Studies Essentials", 172, "https://cdn.example.com/videos/business-studies-3-11.mp4" },
                    { 424, null, 51, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 1.", false, true, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Business Studies Essentials", 173, null },
                    { 425, "https://cdn.example.com/files/business-studies-4-2.pdf", 51, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 2.", false, false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Business Studies Essentials", 173, null },
                    { 426, null, 51, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 3.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Business Studies Essentials", 173, null },
                    { 427, null, 51, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 4.", false, false, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Business Studies Essentials", 174, null },
                    { 428, null, 51, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 5.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Business Studies Essentials", 174, null },
                    { 429, null, 51, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 6.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Business Studies Essentials", 175, "https://cdn.example.com/videos/business-studies-4-6.mp4" },
                    { 430, "https://cdn.example.com/files/business-studies-4-7.pdf", 51, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 7.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Business Studies Essentials", 175, "https://cdn.example.com/videos/business-studies-4-7.mp4" },
                    { 431, null, 51, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 8.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Business Studies Essentials", 176, "https://cdn.example.com/videos/business-studies-4-8.mp4" },
                    { 432, "https://cdn.example.com/files/business-studies-4-9.pdf", 51, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 9.", false, false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Business Studies Essentials", 176, "https://cdn.example.com/videos/business-studies-4-9.mp4" },
                    { 433, "https://cdn.example.com/files/business-studies-5-1.pdf", 52, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 1.", false, true, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Business Studies Essentials", 177, null },
                    { 434, null, 52, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 2.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Business Studies Essentials", 177, "https://cdn.example.com/videos/business-studies-5-2.mp4" },
                    { 435, "https://cdn.example.com/files/business-studies-5-3.pdf", 52, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 3.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Business Studies Essentials", 177, null },
                    { 436, null, 52, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 4.", false, false, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Business Studies Essentials", 178, null },
                    { 437, null, 52, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 5.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Business Studies Essentials", 178, "https://cdn.example.com/videos/business-studies-5-5.mp4" },
                    { 438, null, 52, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 6.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Business Studies Essentials", 179, null },
                    { 439, null, 52, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 7.", false, false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Business Studies Essentials", 179, "https://cdn.example.com/videos/business-studies-5-7.mp4" },
                    { 440, "https://cdn.example.com/files/business-studies-5-8.pdf", 52, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 8.", false, false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Business Studies Essentials", 180, null },
                    { 441, "https://cdn.example.com/files/business-studies-5-9.pdf", 52, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for business studies lesson 9.", false, false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Business Studies Essentials", 180, "https://cdn.example.com/videos/business-studies-5-9.mp4" },
                    { 442, null, 53, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 1.", false, true, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Computer Science Essentials", 181, "https://cdn.example.com/videos/computer-science-1-1.mp4" },
                    { 443, null, 53, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 2.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Computer Science Essentials", 181, null },
                    { 444, null, 53, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 3.", false, false, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Computer Science Essentials", 182, "https://cdn.example.com/videos/computer-science-1-3.mp4" },
                    { 445, null, 53, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 4.", false, false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Computer Science Essentials", 182, "https://cdn.example.com/videos/computer-science-1-4.mp4" },
                    { 446, null, 53, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 5.", false, false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Computer Science Essentials", 182, null },
                    { 447, "https://cdn.example.com/files/computer-science-1-6.pdf", 53, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 6.", false, false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Computer Science Essentials", 183, "https://cdn.example.com/videos/computer-science-1-6.mp4" },
                    { 448, "https://cdn.example.com/files/computer-science-1-7.pdf", 53, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 7.", false, false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Computer Science Essentials", 183, "https://cdn.example.com/videos/computer-science-1-7.mp4" },
                    { 449, "https://cdn.example.com/files/computer-science-2-1.pdf", 54, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 1.", false, true, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Computer Science Essentials", 184, "https://cdn.example.com/videos/computer-science-2-1.mp4" },
                    { 450, null, 54, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 2.", false, false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Computer Science Essentials", 184, null },
                    { 451, "https://cdn.example.com/files/computer-science-2-3.pdf", 54, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 3.", false, false, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Computer Science Essentials", 185, null },
                    { 452, "https://cdn.example.com/files/computer-science-2-4.pdf", 54, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 4.", false, false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Computer Science Essentials", 185, null },
                    { 453, "https://cdn.example.com/files/computer-science-2-5.pdf", 54, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 5.", false, false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Computer Science Essentials", 186, "https://cdn.example.com/videos/computer-science-2-5.mp4" },
                    { 454, "https://cdn.example.com/files/computer-science-2-6.pdf", 54, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 6.", false, false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Computer Science Essentials", 186, "https://cdn.example.com/videos/computer-science-2-6.mp4" },
                    { 455, null, 54, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 7.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 7: Computer Science Essentials", 186, null },
                    { 456, "https://cdn.example.com/files/computer-science-2-8.pdf", 54, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 8.", false, false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Computer Science Essentials", 187, "https://cdn.example.com/videos/computer-science-2-8.mp4" },
                    { 457, "https://cdn.example.com/files/computer-science-2-9.pdf", 54, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 9.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Computer Science Essentials", 187, "https://cdn.example.com/videos/computer-science-2-9.mp4" },
                    { 458, "https://cdn.example.com/files/computer-science-2-10.pdf", 54, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 10.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 10: Computer Science Essentials", 187, null },
                    { 459, null, 55, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 1.", false, true, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Computer Science Essentials", 188, "https://cdn.example.com/videos/computer-science-3-1.mp4" },
                    { 460, null, 55, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 2.", false, false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Computer Science Essentials", 188, "https://cdn.example.com/videos/computer-science-3-2.mp4" },
                    { 461, null, 55, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 3.", false, false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Computer Science Essentials", 188, "https://cdn.example.com/videos/computer-science-3-3.mp4" },
                    { 462, null, 55, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 4.", false, false, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Computer Science Essentials", 189, null },
                    { 463, "https://cdn.example.com/files/computer-science-3-5.pdf", 55, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 5.", false, false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Computer Science Essentials", 189, null },
                    { 464, null, 55, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 6.", false, false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Computer Science Essentials", 190, "https://cdn.example.com/videos/computer-science-3-6.mp4" },
                    { 465, null, 55, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 7.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Computer Science Essentials", 190, "https://cdn.example.com/videos/computer-science-3-7.mp4" },
                    { 466, "https://cdn.example.com/files/computer-science-3-8.pdf", 55, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 8.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Computer Science Essentials", 191, null },
                    { 467, "https://cdn.example.com/files/computer-science-3-9.pdf", 55, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 9.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Computer Science Essentials", 191, null },
                    { 468, "https://cdn.example.com/files/computer-science-3-10.pdf", 55, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 10.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 10: Computer Science Essentials", 191, "https://cdn.example.com/videos/computer-science-3-10.mp4" },
                    { 469, null, 56, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 1.", false, true, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Computer Science Essentials", 192, null },
                    { 470, null, 56, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 2.", false, false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Computer Science Essentials", 192, "https://cdn.example.com/videos/computer-science-4-2.mp4" },
                    { 471, "https://cdn.example.com/files/computer-science-4-3.pdf", 56, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 3.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Computer Science Essentials", 192, "https://cdn.example.com/videos/computer-science-4-3.mp4" },
                    { 472, "https://cdn.example.com/files/computer-science-4-4.pdf", 56, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 4.", false, false, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Computer Science Essentials", 193, "https://cdn.example.com/videos/computer-science-4-4.mp4" },
                    { 473, "https://cdn.example.com/files/computer-science-4-5.pdf", 56, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 5.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Computer Science Essentials", 193, null },
                    { 474, null, 56, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 6.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Computer Science Essentials", 194, "https://cdn.example.com/videos/computer-science-4-6.mp4" },
                    { 475, "https://cdn.example.com/files/computer-science-4-7.pdf", 56, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 7.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Computer Science Essentials", 194, "https://cdn.example.com/videos/computer-science-4-7.mp4" },
                    { 476, "https://cdn.example.com/files/computer-science-4-8.pdf", 56, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 8.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Computer Science Essentials", 194, null },
                    { 477, null, 56, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 9.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 9: Computer Science Essentials", 195, null },
                    { 478, null, 56, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 10.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 10: Computer Science Essentials", 195, null },
                    { 479, "https://cdn.example.com/files/computer-science-5-1.pdf", 57, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 1.", false, true, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Computer Science Essentials", 196, "https://cdn.example.com/videos/computer-science-5-1.mp4" },
                    { 480, null, 57, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 2.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Computer Science Essentials", 196, "https://cdn.example.com/videos/computer-science-5-2.mp4" },
                    { 481, "https://cdn.example.com/files/computer-science-5-3.pdf", 57, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 3.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Computer Science Essentials", 196, "https://cdn.example.com/videos/computer-science-5-3.mp4" },
                    { 482, null, 57, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 4.", false, false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Computer Science Essentials", 197, null },
                    { 483, "https://cdn.example.com/files/computer-science-5-5.pdf", 57, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 5.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Computer Science Essentials", 197, "https://cdn.example.com/videos/computer-science-5-5.mp4" },
                    { 484, "https://cdn.example.com/files/computer-science-5-6.pdf", 57, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 6.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Computer Science Essentials", 198, null },
                    { 485, null, 57, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 7.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Computer Science Essentials", 198, "https://cdn.example.com/videos/computer-science-5-7.mp4" },
                    { 486, "https://cdn.example.com/files/computer-science-5-8.pdf", 57, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 8.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 8: Computer Science Essentials", 199, null },
                    { 487, null, 57, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 9.", false, false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 9: Computer Science Essentials", 199, null },
                    { 488, null, 58, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 1.", false, true, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Computer Science Essentials", 200, "https://cdn.example.com/videos/computer-science-6-1.mp4" },
                    { 489, "https://cdn.example.com/files/computer-science-6-2.pdf", 58, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 2.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Computer Science Essentials", 200, null },
                    { 490, null, 58, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 3.", false, false, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Computer Science Essentials", 201, "https://cdn.example.com/videos/computer-science-6-3.mp4" },
                    { 491, "https://cdn.example.com/files/computer-science-6-4.pdf", 58, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 4.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Computer Science Essentials", 201, null },
                    { 492, null, 58, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 5.", false, false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Computer Science Essentials", 201, null },
                    { 493, null, 58, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 6.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Computer Science Essentials", 202, null },
                    { 494, null, 58, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 7.", false, false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Computer Science Essentials", 202, "https://cdn.example.com/videos/computer-science-6-7.mp4" },
                    { 495, null, 58, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for computer science lesson 8.", false, false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Computer Science Essentials", 202, "https://cdn.example.com/videos/computer-science-6-8.mp4" },
                    { 496, null, 59, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 1.", false, true, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Art & Design Essentials", 203, "https://cdn.example.com/videos/art-&-design-1-1.mp4" },
                    { 497, "https://cdn.example.com/files/art-&-design-1-2.pdf", 59, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 2.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Art & Design Essentials", 203, null },
                    { 498, "https://cdn.example.com/files/art-&-design-1-3.pdf", 59, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 3.", false, false, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Art & Design Essentials", 204, null },
                    { 499, "https://cdn.example.com/files/art-&-design-1-4.pdf", 59, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 4.", false, false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Art & Design Essentials", 204, null },
                    { 500, null, 59, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 5.", false, false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Art & Design Essentials", 205, "https://cdn.example.com/videos/art-&-design-1-5.mp4" },
                    { 501, "https://cdn.example.com/files/art-&-design-1-6.pdf", 59, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 6.", false, false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Art & Design Essentials", 205, null },
                    { 502, null, 60, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 1.", false, true, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Art & Design Essentials", 206, null },
                    { 503, null, 60, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 2.", false, false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Art & Design Essentials", 206, "https://cdn.example.com/videos/art-&-design-2-2.mp4" },
                    { 504, "https://cdn.example.com/files/art-&-design-2-3.pdf", 60, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 3.", false, false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Art & Design Essentials", 206, null },
                    { 505, null, 60, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 4.", false, false, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Art & Design Essentials", 207, null },
                    { 506, "https://cdn.example.com/files/art-&-design-2-5.pdf", 60, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 5.", false, false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Art & Design Essentials", 207, null },
                    { 507, "https://cdn.example.com/files/art-&-design-2-6.pdf", 60, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 6.", false, false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: Art & Design Essentials", 207, null },
                    { 508, "https://cdn.example.com/files/art-&-design-2-7.pdf", 60, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 7.", false, false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Art & Design Essentials", 208, "https://cdn.example.com/videos/art-&-design-2-7.mp4" },
                    { 509, null, 60, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 8.", false, false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Art & Design Essentials", 208, null },
                    { 510, "https://cdn.example.com/files/art-&-design-3-1.pdf", 61, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 1.", false, true, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Art & Design Essentials", 209, null },
                    { 511, "https://cdn.example.com/files/art-&-design-3-2.pdf", 61, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 2.", false, false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Art & Design Essentials", 209, "https://cdn.example.com/videos/art-&-design-3-2.mp4" },
                    { 512, null, 61, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 3.", false, false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 3: Art & Design Essentials", 209, "https://cdn.example.com/videos/art-&-design-3-3.mp4" },
                    { 513, null, 61, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 4.", false, false, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 4: Art & Design Essentials", 210, null },
                    { 514, null, 61, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 5.", false, false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 5: Art & Design Essentials", 210, null },
                    { 515, null, 61, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 6.", false, false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 6: Art & Design Essentials", 210, null },
                    { 516, null, 61, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 7.", false, false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 7: Art & Design Essentials", 211, null },
                    { 517, "https://cdn.example.com/files/art-&-design-3-8.pdf", 61, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 8.", false, false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 8: Art & Design Essentials", 211, "https://cdn.example.com/videos/art-&-design-3-8.mp4" },
                    { 518, null, 61, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 9.", false, false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 9: Art & Design Essentials", 211, null },
                    { 519, null, 62, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 1.", false, true, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Art & Design Essentials", 212, null },
                    { 520, "https://cdn.example.com/files/art-&-design-4-2.pdf", 62, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 2.", false, false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Art & Design Essentials", 212, "https://cdn.example.com/videos/art-&-design-4-2.mp4" },
                    { 521, null, 62, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 3.", false, false, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Art & Design Essentials", 213, null },
                    { 522, null, 62, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 4.", false, false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Art & Design Essentials", 213, null },
                    { 523, "https://cdn.example.com/files/art-&-design-4-5.pdf", 62, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 5.", false, false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 5: Art & Design Essentials", 213, null },
                    { 524, "https://cdn.example.com/files/art-&-design-4-6.pdf", 62, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 6.", false, false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 6: Art & Design Essentials", 214, null },
                    { 525, "https://cdn.example.com/files/art-&-design-4-7.pdf", 62, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 7.", false, false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 7: Art & Design Essentials", 214, null },
                    { 526, null, 62, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 8.", false, false, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 3, "Lesson 8: Art & Design Essentials", 214, "https://cdn.example.com/videos/art-&-design-4-8.mp4" },
                    { 527, "https://cdn.example.com/files/art-&-design-5-1.pdf", 63, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 1.", false, true, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 1: Art & Design Essentials", 215, null },
                    { 528, "https://cdn.example.com/files/art-&-design-5-2.pdf", 63, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 2.", false, false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 2: Art & Design Essentials", 215, "https://cdn.example.com/videos/art-&-design-5-2.mp4" },
                    { 529, "https://cdn.example.com/files/art-&-design-5-3.pdf", 63, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 3.", false, false, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 3: Art & Design Essentials", 216, null },
                    { 530, "https://cdn.example.com/files/art-&-design-5-4.pdf", 63, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 4.", false, false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 4: Art & Design Essentials", 216, "https://cdn.example.com/videos/art-&-design-5-4.mp4" },
                    { 531, "https://cdn.example.com/files/art-&-design-5-5.pdf", 63, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 5.", false, false, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 1, "Lesson 5: Art & Design Essentials", 217, null },
                    { 532, null, 63, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", "Core concepts for art & design lesson 6.", false, false, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Seed", 2, "Lesson 6: Art & Design Essentials", 217, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_AttemptId",
                table: "AttemptAnswers",
                column: "AttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_QuestionId",
                table: "AttemptAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_SelectedOptionId",
                table: "AttemptAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCoursePackage_PackagesId",
                table: "CourseCoursePackage",
                column: "PackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AccountId",
                table: "Devices",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_AccountId",
                table: "Enrollments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonCompletions_EnrollmentId_LessonId",
                table: "LessonCompletions",
                columns: new[] { "EnrollmentId", "LessonId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonCompletions_LessonId",
                table: "LessonCompletions",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_UnitId",
                table: "Lessons",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LivestreamRegistrations_AccountId",
                table: "LivestreamRegistrations",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LivestreamRegistrations_LivestreamId",
                table: "LivestreamRegistrations",
                column: "LivestreamId");

            migrationBuilder.CreateIndex(
                name: "IX_Livestreams_AccountId",
                table: "Livestreams",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Livestreams_CourseId",
                table: "Livestreams",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Livestreams_TeacherId",
                table: "Livestreams",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId",
                table: "Orders",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_EnrollmentId",
                table: "Progresses",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CourseId",
                table: "Questions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_AccountId",
                table: "QuizAttempts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_QuizId",
                table: "QuizAttempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuestionId",
                table: "QuizQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CourseId",
                table: "Quizzes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AccountId",
                table: "RefreshTokens",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AccountId",
                table: "Submissions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CourseId",
                table: "Units",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttemptAnswers");

            migrationBuilder.DropTable(
                name: "CourseCoursePackage");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "LessonCompletions");

            migrationBuilder.DropTable(
                name: "LivestreamRegistrations");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "QuizAttempts");

            migrationBuilder.DropTable(
                name: "CoursePackages");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Livestreams");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
