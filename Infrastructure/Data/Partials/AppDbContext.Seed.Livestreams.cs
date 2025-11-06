using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public partial class AppDbContext
    {
        private static void ConfigureLivestreams(ModelBuilder modelBuilder)
        {
            var livestreams = new List<object>();
            var baseSchedule = new DateTime(2024, 3, 1, 13, 0, 0, DateTimeKind.Utc);

            var seedItems = new (int CourseId, int TeacherId, string Title, int DayOffset, int HourOffset, decimal Price)[]
            {
                (1, 6, "Live Workshop: Algebra Essentials", 5, 0, 150_000m),
                (2, 7, "Physics Lab: Motion & Forces Q&A", 8, 2, 180_000m),
                (3, 8, "Chemistry Experiments: Reaction Rates", 11, -1, 170_000m),
                (4, 9, "Biology Clinic: Genetics Deep Dive", 14, 1, 165_000m),
                (5, 10, "English Language: Writing for Impact", 17, 0, 155_000m),
                (6, 7, "English Literature: Poetry Study Hall", 20, -2, 145_000m),
                (7, 8, "History Roundtable: Cold War Events", 23, 1, 160_000m),
                (8, 6, "Geography Spotlight: Climate Change", 26, 0, 150_000m),
                (9, 9, "Economics Insights: Market Structures", 29, 2, 175_000m),
                (10, 10, "Business Studies: Marketing Pitches", 32, -1, 165_000m),
                (11, 6, "Computer Science: Algorithm Clinic", 35, 1, 190_000m),
                (12, 7, "Art & Design Critique Session", 38, 0, 140_000m)
            };

            for (var index = 0; index < seedItems.Length; index++)
            {
                var id = index + 1;
                var item = seedItems[index];
                var schedule = baseSchedule.AddDays(item.DayOffset).AddHours(item.HourOffset);
                var createdAt = schedule.AddDays(-5);

                livestreams.Add(new
                {
                    Id = id,
                    item.CourseId,
                    item.TeacherId,
                    item.Title,
                    Schedule = schedule,
                    Price = item.Price,
                    CreatedAt = createdAt,
                    CreatedBy = "Seed",
                    ModifiedAt = createdAt,
                    ModifiedBy = "Seed",
                    IsDeleted = false
                });
            }

            modelBuilder.Entity<Livestream>().HasData(livestreams.ToArray());
        }
    }
}
