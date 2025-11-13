using Domain.Enums;

namespace Application.Payments.DTOs
{
    public class CheckoutRequestDTO
    {
        public PaymentChannel? Channel { get; set; }
    }
}
