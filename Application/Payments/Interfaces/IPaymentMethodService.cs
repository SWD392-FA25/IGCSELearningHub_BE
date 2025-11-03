using Application.DTOs.Payments;
using Application.Wrappers;

namespace Application.Payments.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<Result<IEnumerable<PaymentMethodDTO>>> GetAllAsync();
        Task<Result<IEnumerable<PaymentMethodDTO>>> GetActiveAsync();
        Task<Result<bool>> SetStatusAsync(int id, bool isActive);
    }
}
