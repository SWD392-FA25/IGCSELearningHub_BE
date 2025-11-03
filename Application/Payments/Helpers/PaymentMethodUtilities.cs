using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Payments.Helpers
{
    public static class PaymentMethodUtilities
    {
        public const string VnPayMethodName = "VNPay";
        public const string CashMethodName = "Cash";

        private static readonly IReadOnlyList<(string Name, string Description, bool IsActive)> DefaultMethods = new[]
        {
            (VnPayMethodName, "VNPay payment gateway", true),
            (CashMethodName, "Thanh toán tiền mặt", true)
        };

        public static async Task EnsureDefaultMethodsAsync(IUnitOfWork uow, ILogger? logger = null)
        {
            var existingNames = await uow.PaymentMethodRepository
                .GetAllQueryable()
                .Select(x => x.PaymentMethodName)
                .ToListAsync();

            var missing = DefaultMethods
                .Where(dm => !existingNames.Contains(dm.Name, StringComparer.OrdinalIgnoreCase))
                .ToList();

            if (missing.Count == 0) return;

            foreach (var (name, description, isActive) in missing)
            {
                await uow.PaymentMethodRepository.AddAsync(new PaymentMethod
                {
                    PaymentMethodName = name,
                    PaymentMethodDescription = description,
                    IsActive = isActive
                });
            }

            try
            {
                await uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Failed to seed default payment methods.");
            }
        }

        public static async Task<PaymentMethod> EnsureMethodAsync(IUnitOfWork uow, string name, string description, bool isActiveByDefault, ILogger? logger = null)
        {
            var method = await uow.PaymentMethodRepository.FindOneAsync(x => x.PaymentMethodName == name);
            if (method != null) return method;

            method = new PaymentMethod
            {
                PaymentMethodName = name,
                PaymentMethodDescription = description,
                IsActive = isActiveByDefault
            };

            await uow.PaymentMethodRepository.AddAsync(method);

            try
            {
                await uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Failed to create payment method {Name}.", name);
            }

            return method;
        }
    }
}
