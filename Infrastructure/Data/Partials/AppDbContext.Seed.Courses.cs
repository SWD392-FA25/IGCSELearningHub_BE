using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public partial class AppDbContext
    {
        private static void ConfigureCourses(ModelBuilder modelBuilder)
        {
            var subjects = new List<(string Name, string Description, decimal BasePrice, SubjectGroup Group, int CourseCount)>
            {
                ("Mathematics", "Mathematics curriculum covering algebra, geometry and statistics", 1_950_000m, SubjectGroup.Mathematics, 6),
                ("Physics", "Physics curriculum covering mechanics, waves, electricity and nuclear science", 2_150_000m, SubjectGroup.Sciences, 6),
                ("Chemistry", "Chemistry course covering atomic structure, bonding, energetics and kinetics", 2_250_000m, SubjectGroup.Sciences, 5),
                ("Biology", "Biology course covering cell biology, genetics, ecology and evolution", 2_100_000m, SubjectGroup.Sciences, 5),
                ("English Language", "English language course focusing on grammar, comprehension and writing", 1_850_000m, SubjectGroup.Languages, 5),
                ("English Literature", "Literature course analyzing prose, poetry and drama", 1_900_000m, SubjectGroup.Languages, 5),
                ("History", "History course covering world history and modern developments", 1_700_000m, SubjectGroup.HumanitiesAndSocialSciences, 5),
                ("Geography", "Geography course covering physical and human geography", 1_650_000m, SubjectGroup.HumanitiesAndSocialSciences, 5),
                ("Economics", "Economics course covering microeconomics, macroeconomics and development", 2_050_000m, SubjectGroup.HumanitiesAndSocialSciences, 5),
                ("Business Studies", "Business course covering entrepreneurship, marketing, finance and management", 2_150_000m, SubjectGroup.CreativeTechnicalAndVocational, 5),
                ("Computer Science", "Computer science course covering programming, algorithms and data structures", 2_450_000m, SubjectGroup.CreativeTechnicalAndVocational, 6),
                ("Art & Design", "Art & design course covering creative techniques and art history", 1_800_000m, SubjectGroup.CreativeTechnicalAndVocational, 5)
            };

            var random = new Random(42);
            var baseDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var levels = new[] { "Foundation", "Intermediate", "Advanced" };

            var courses = new List<object>();
            var curricula = new List<object>();
            var lessons = new List<object>();
            var assignments = new List<object>();
            var quizzes = new List<object>();

            var courseId = 1;
            var curriculumId = 1;
            var lessonId = 1;
            var assignmentId = 1;
            var quizId = 1;

            foreach (var subject in subjects)
            {
                for (var idx = 1; idx <= subject.CourseCount; idx++)
                {
                    var title = $"IGCSE {subject.Name} {idx}";
                    var description = $"{subject.Description} (module {idx}).";
                    var info = $"Detailed overview for {subject.Name} module {idx}.";
                    var level = idx <= 3 ? levels[idx - 1] : levels[random.Next(levels.Length)];
                    var price = subject.BasePrice + random.Next(0, 6) * 100_000m;
                    var createdAt = baseDate.AddDays(courseId);

                    courses.Add(new
                    {
                        Id = courseId,
                        Title = title,
                        Description = description,
                        Info = info,
                        SubjectGroup = subject.Group,
                        Level = level,
                        Price = price,
                        CreatedAt = createdAt,
                        CreatedBy = "Seed",
                        ModifiedAt = createdAt,
                        ModifiedBy = "Seed",
                        IsDeleted = false
                    });

                    var moduleCount = random.Next(3, 5);
                    var lessonCounter = 1;
                    for (var module = 1; module <= moduleCount; module++)
                    {
                        var curriculumTitle = $"Module {module}: {subject.Name} Unit";
                        var curriculumDesc = $"Focus topics for {subject.Name} module {module}.";
                        var curriculumCreated = createdAt.AddDays(module);

                        curricula.Add(new
                        {
                            Id = curriculumId,
                            CourseId = courseId,
                            Title = curriculumTitle,
                            Description = curriculumDesc,
                            OrderIndex = module,
                            CreatedAt = curriculumCreated,
                            CreatedBy = "Seed",
                            ModifiedAt = curriculumCreated,
                            ModifiedBy = "Seed",
                            IsDeleted = false
                        });

                        var lessonsInModule = random.Next(2, 4);
                        for (var order = 1; order <= lessonsInModule; order++)
                        {
                            var slug = subject.Name.ToLower().Replace(" ", "-");
                            var lessonCreated = curriculumCreated.AddDays(order);

                            lessons.Add(new
                            {
                                Id = lessonId,
                                CourseId = courseId,
                                CurriculumId = curriculumId,
                                Title = $"Lesson {lessonCounter}: {subject.Name} Essentials",
                                Description = $"Core concepts for {subject.Name.ToLower()} lesson {lessonCounter}.",
                                VideoUrl = random.Next(0, 2) == 0 ? (string?)null : $"https://cdn.example.com/videos/{slug}-{idx}-{lessonCounter}.mp4",
                                AttachmentUrl = random.Next(0, 2) == 0 ? (string?)null : $"https://cdn.example.com/files/{slug}-{idx}-{lessonCounter}.pdf",
                                OrderIndex = order,
                                IsFreePreview = module == 1 && order == 1,
                                CreatedAt = lessonCreated,
                                CreatedBy = "Seed",
                                ModifiedAt = lessonCreated,
                                ModifiedBy = "Seed",
                                IsDeleted = false
                            });
                            lessonId++;
                            lessonCounter++;
                        }

                        curriculumId++;
                    }

                    for (var a = 1; a <= 2; a++)
                    {
                        var assignCreated = createdAt.AddDays(moduleCount + a + 2);
                        assignments.Add(new
                        {
                            Id = assignmentId,
                            CourseId = courseId,
                            Title = $"Assignment {a}: {subject.Name} Project",
                            Description = $"{subject.Name} assignment {a} focusing on key skills.",
                            CreatedAt = assignCreated,
                            CreatedBy = "Seed",
                            ModifiedAt = assignCreated,
                            ModifiedBy = "Seed",
                            IsDeleted = false
                        });
                        assignmentId++;
                    }

                    for (var q = 1; q <= 2; q++)
                    {
                        var quizCreated = createdAt.AddDays(moduleCount + q + 5);
                        quizzes.Add(new
                        {
                            Id = quizId,
                            CourseId = courseId,
                            Title = $"{subject.Name} Quiz {q}",
                            TotalQuestions = random.Next(10, 16),
                            CreatedAt = quizCreated,
                            CreatedBy = "Seed",
                            ModifiedAt = quizCreated,
                            ModifiedBy = "Seed",
                            IsDeleted = false
                        });
                        quizId++;
                    }

                    courseId++;
                }
            }

            modelBuilder.Entity<Course>().HasData(courses.ToArray());
            modelBuilder.Entity<Curriculum>().HasData(curricula.ToArray());
            modelBuilder.Entity<Lesson>().HasData(lessons.ToArray());
            modelBuilder.Entity<Assignment>().HasData(assignments.ToArray());
            modelBuilder.Entity<Quiz>().HasData(quizzes.ToArray());
        }
    }
}
