using Domain.Enums;

namespace Application.Payments.DTOs
{
    public sealed class PaymentResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? TransactionRef { get; set; }
        public string? BankCode { get; set; }
        public string? PayDate { get; set; }
        public string RawQuery { get; set; } = string.Empty;

        // mapping sang domain:
        public decimal Amount { get; set; } // VND
        public string Currency { get; set; } = "VND";
        public PaymentStatus Status { get; set; }
        public int OrderId { get; set; }
        public PaymentChannel Channel { get; set; } = PaymentChannel.Web;
    }
}
