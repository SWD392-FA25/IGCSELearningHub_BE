using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCoursesAndLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
-- Seed Course: IGCSE Mathematics
IF NOT EXISTS (SELECT 1 FROM Courses WHERE Title = 'IGCSE Mathematics' AND IsDeleted = 0)
BEGIN
INSERT INTO Courses (Title, Description, Level, Price, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy, IsDeleted)
VALUES ('IGCSE Mathematics', N'Algebra, Geometry, Statistics', 'Beginner', 1990000,
SYSUTCDATETIME(), 'Seed', SYSUTCDATETIME(), 'Seed', 0);
END;

DECLARE @MathId INT = (SELECT TOP 1 Id FROM Courses WHERE Title = 'IGCSE Mathematics' AND IsDeleted = 0 ORDER BY Id);

IF @MathId IS NOT NULL
BEGIN
IF NOT EXISTS (SELECT 1 FROM Lessons WHERE CourseId = @MathId AND Title = 'Introduction' AND IsDeleted = 0)
INSERT INTO Lessons (CourseId, Title, Description, VideoUrl, AttachmentUrl, OrderIndex, IsFreePreview, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy, IsDeleted)
VALUES (@MathId, 'Introduction', N'Tổng quan khóa học', NULL, NULL, 1, 1, SYSUTCDATETIME(), 'Seed', SYSUTCDATETIME(), 'Seed', 0);

IF NOT EXISTS (SELECT 1 FROM Lessons WHERE CourseId = @MathId AND Title = 'Algebra Basics' AND IsDeleted = 0)
    INSERT INTO Lessons (CourseId, Title, Description, VideoUrl, AttachmentUrl, OrderIndex, IsFreePreview, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy, IsDeleted)
    VALUES (@MathId, 'Algebra Basics', N'Biến và biểu thức', 'https://cdn.example.com/videos/algebra.mp4', NULL, 2, 0, SYSUTCDATETIME(), 'Seed', SYSUTCDATETIME(), 'Seed', 0);

IF NOT EXISTS (SELECT 1 FROM Lessons WHERE CourseId = @MathId AND Title = 'Geometry Foundations' AND IsDeleted = 0)
    INSERT INTO Lessons (CourseId, Title, Description, VideoUrl, AttachmentUrl, OrderIndex, IsFreePreview, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy, IsDeleted)
    VALUES (@MathId, 'Geometry Foundations', N'Góc, tam giác, đa giác', NULL, 'https://cdn.example.com/files/geometry.pdf', 3, 0, SYSUTCDATETIME(), 'Seed', SYSUTCDATETIME(), 'Seed', 0);
END;

-- Seed Course: IGCSE Physics
IF NOT EXISTS (SELECT 1 FROM Courses WHERE Title = 'IGCSE Physics' AND IsDeleted = 0)
BEGIN
INSERT INTO Courses (Title, Description, Level, Price, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy, IsDeleted)
VALUES ('IGCSE Physics', N'Mechanics & Waves', 'Intermediate', 2490000,
SYSUTCDATETIME(), 'Seed', SYSUTCDATETIME(), 'Seed', 0);
END;

DECLARE @PhyId INT = (SELECT TOP 1 Id FROM Courses WHERE Title = 'IGCSE Physics' AND IsDeleted = 0 ORDER BY Id);

IF @PhyId IS NOT NULL
BEGIN
IF NOT EXISTS (SELECT 1 FROM Lessons WHERE CourseId = @PhyId AND Title = 'Kinematics Intro' AND IsDeleted = 0)
INSERT INTO Lessons (CourseId, Title, Description, VideoUrl, AttachmentUrl, OrderIndex, IsFreePreview, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy, IsDeleted)
VALUES (@PhyId, 'Kinematics Intro', N'Chuyển động thẳng', 'https://cdn.example.com/videos/kinematics.mp4', NULL, 1, 1, SYSUTCDATETIME(), 'Seed', SYSUTCDATETIME(), 'Seed', 0);

IF NOT EXISTS (SELECT 1 FROM Lessons WHERE CourseId = @PhyId AND Title = 'Dynamics Basics' AND IsDeleted = 0)
    INSERT INTO Lessons (CourseId, Title, Description, VideoUrl, AttachmentUrl, OrderIndex, IsFreePreview, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy, IsDeleted)
    VALUES (@PhyId, 'Dynamics Basics', N'Lực và định luật Newton', NULL, NULL, 2, 0, SYSUTCDATETIME(), 'Seed', SYSUTCDATETIME(), 'Seed', 0);

IF NOT EXISTS (SELECT 1 FROM Lessons WHERE CourseId = @PhyId AND Title = 'Waves Overview' AND IsDeleted = 0)
    INSERT INTO Lessons (CourseId, Title, Description, VideoUrl, AttachmentUrl, OrderIndex, IsFreePreview, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy, IsDeleted)
    VALUES (@PhyId, 'Waves Overview', N'Đại cương sóng cơ', NULL, 'https://cdn.example.com/files/waves.pdf', 3, 0, SYSUTCDATETIME(), 'Seed', SYSUTCDATETIME(), 'Seed', 0);
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE L
FROM Lessons L
WHERE L.IsDeleted = 0
AND EXISTS (SELECT 1 FROM Courses C WHERE C.Id = L.CourseId AND C.Title IN ('IGCSE Mathematics','IGCSE Physics'));

DELETE FROM Courses
WHERE Title IN ('IGCSE Mathematics','IGCSE Physics') AND IsDeleted = 0;
");
        }
    }
}
