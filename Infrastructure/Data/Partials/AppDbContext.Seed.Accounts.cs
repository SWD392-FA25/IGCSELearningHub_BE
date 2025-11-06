using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public partial class AppDbContext
    {
        private static void ConfigureAccounts(ModelBuilder modelBuilder)
        {
            var accounts = new List<object>();
            var baseTime = new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc);
            const string passwordHash = "$2a$12$S2s12f8mDSvk3zXqG7KXy1BPeGS.2o0BSXS1SZXUMKOI5oT7Ck9Da";

            var seedAccounts = new (string UserName, string FullName, string Email, AccountRole Role, string Gender, string Phone)[]
            {
                ("admin.owen", "Owen Tran", "owen.tran@igcsehub.com", AccountRole.Admin, "Male", "0987000001"),
                ("admin.linh", "Linh Pham", "linh.pham@igcsehub.com", AccountRole.Admin, "Female", "0987000002"),
                ("admin.michael", "Michael Le", "michael.le@igcsehub.com", AccountRole.Admin, "Male", "0987000003"),
                ("admin.anh", "Anh Nguyen", "anh.nguyen@igcsehub.com", AccountRole.Admin, "Female", "0987000004"),
                ("admin.olivia", "Olivia Vu", "olivia.vu@igcsehub.com", AccountRole.Admin, "Female", "0987000005"),

                ("teacher.mai", "Mai Hoang", "mai.hoang@igcsehub.com", AccountRole.Teacher, "Female", "0987111101"),
                ("teacher.james", "James Dao", "james.dao@igcsehub.com", AccountRole.Teacher, "Male", "0987111102"),
                ("teacher.khang", "Khang Bui", "khang.bui@igcsehub.com", AccountRole.Teacher, "Male", "0987111103"),
                ("teacher.rachel", "Rachel Do", "rachel.do@igcsehub.com", AccountRole.Teacher, "Female", "0987111104"),
                ("teacher.sophia", "Sophia Truong", "sophia.truong@igcsehub.com", AccountRole.Teacher, "Female", "0987111105"),

                ("student.huy", "Huy Nguyen", "huy.nguyen@student.igcsehub.com", AccountRole.Student, "Male", "0987222201"),
                ("student.emma", "Emma Phan", "emma.phan@student.igcsehub.com", AccountRole.Student, "Female", "0987222202"),
                ("student.minhchau", "Minh Chau Vo", "minhchau.vo@student.igcsehub.com", AccountRole.Student, "Female", "0987222203"),
                ("student.lucas", "Lucas Tran", "lucas.tran@student.igcsehub.com", AccountRole.Student, "Male", "0987222204"),
                ("student.hana", "Hana Le", "hana.le@student.igcsehub.com", AccountRole.Student, "Female", "0987222205"),
                ("student.david", "David Ho", "david.ho@student.igcsehub.com", AccountRole.Student, "Male", "0987222206"),
                ("student.quynh", "Quynh Nguyen", "quynh.nguyen@student.igcsehub.com", AccountRole.Student, "Female", "0987222207"),
                ("student.ryan", "Ryan Pham", "ryan.pham@student.igcsehub.com", AccountRole.Student, "Male", "0987222208"),
                ("student.julia", "Julia Dang", "julia.dang@student.igcsehub.com", AccountRole.Student, "Female", "0987222209"),
                ("student.long", "Long Truong", "long.truong@student.igcsehub.com", AccountRole.Student, "Male", "0987222210")
            };

            for (var index = 0; index < seedAccounts.Length; index++)
            {
                var id = index + 1;
                var account = seedAccounts[index];
                var timestamp = baseTime.AddHours(id);

                accounts.Add(new
                {
                    Id = id,
                    account.UserName,
                    account.FullName,
                    account.Email,
                    Password = passwordHash,
                    PhoneNumber = account.Phone,
                    Role = account.Role,
                    Gender = account.Gender,
                    Status = "Active",
                    IsExternal = false,
                    ExternalProvider = (string?)null,
                    CreatedAt = timestamp,
                    CreatedBy = "Seed",
                    ModifiedAt = timestamp,
                    ModifiedBy = "Seed",
                    IsDeleted = false
                });
            }

            modelBuilder.Entity<Account>().HasData(accounts.ToArray());
        }
    }
}
