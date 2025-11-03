using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public partial class AppDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            ConfigurePaymentMethods(modelBuilder);
        }

        private static void ConfigurePaymentMethods(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("PaymentMethods");

                entity.Property(e => e.PaymentMethodName)
                    .IsRequired();

                entity.Property(e => e.PaymentMethodDescription)
                    .HasMaxLength(255);

                entity.HasData(
                    new
                    {
                        Id = 1,
                        PaymentMethodName = "VNPay",
                        PaymentMethodDescription = "VNPay payment gateway",
                        IsActive = true,
                        CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        CreatedBy = "System",
                        ModifiedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        ModifiedBy = "System",
                        IsDeleted = false
                    },
                    new
                    {
                        Id = 2,
                        PaymentMethodName = "Cash",
                        PaymentMethodDescription = "Thanh toán tiền mặt",
                        IsActive = true,
                        CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        CreatedBy = "System",
                        ModifiedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        ModifiedBy = "System",
                        IsDeleted = false
                    }
                );
            });
        }
    }
}
