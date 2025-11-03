using Application.Payments.DTOs;
using Application.Wrappers;

namespace Application.Payments.Interfaces
{
    public interface ICashPaymentService
    {
        Task<Result<CashPaymentResultDTO>> ProcessAsync(int orderId, CashPaymentRequestDTO request);
    }
}
