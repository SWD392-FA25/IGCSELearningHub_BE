using System.Linq;
using Application.DTOs.Payments;
using Application.Payments.Helpers;
using Application.Payments.Interfaces;
using Application.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Payments.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<PaymentMethodService> _logger;

        public PaymentMethodService(IUnitOfWork uow, ILogger<PaymentMethodService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<PaymentMethodDTO>>> GetAllAsync()
        {
            await PaymentMethodUtilities.EnsureDefaultMethodsAsync(_uow, _logger);

            var data = await _uow.PaymentMethodRepository
                .GetAllQueryable()
                .OrderBy(x => x.PaymentMethodName)
                .Select(x => new PaymentMethodDTO
                {
                    Id = x.Id,
                    Name = x.PaymentMethodName,
                    Description = x.PaymentMethodDescription,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return Result<IEnumerable<PaymentMethodDTO>>.Success(data, "Payment methods retrieved successfully.");
        }

        public async Task<Result<IEnumerable<PaymentMethodDTO>>> GetActiveAsync()
        {
            await PaymentMethodUtilities.EnsureDefaultMethodsAsync(_uow, _logger);

            var data = await _uow.PaymentMethodRepository
                .GetAllQueryable()
                .Where(x => x.IsActive)
                .OrderBy(x => x.PaymentMethodName)
                .Select(x => new PaymentMethodDTO
                {
                    Id = x.Id,
                    Name = x.PaymentMethodName,
                    Description = x.PaymentMethodDescription,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return Result<IEnumerable<PaymentMethodDTO>>.Success(data, "Active payment methods retrieved successfully.");
        }

        public async Task<Result<bool>> SetStatusAsync(int id, bool isActive)
        {
            await PaymentMethodUtilities.EnsureDefaultMethodsAsync(_uow, _logger);

            var method = await _uow.PaymentMethodRepository.GetByIdAsync(id);
            if (method == null)
            {
                return Result<bool>.Fail("Payment method not found.", 404);
            }

            if (method.IsActive == isActive)
            {
                var message = isActive ? "Payment method already active." : "Payment method already inactive.";
                return Result<bool>.Success(true, message, 200);
            }

            method.IsActive = isActive;
            _uow.PaymentMethodRepository.Update(method);
            await _uow.SaveChangesAsync();

            var successMessage = isActive ? "Payment method activated." : "Payment method deactivated.";
            return Result<bool>.Success(true, successMessage, 200);
        }
    }
}
