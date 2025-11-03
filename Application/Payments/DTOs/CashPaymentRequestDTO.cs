using System.ComponentModel.DataAnnotations;

namespace Application.Payments.DTOs
{
    public class CashPaymentRequestDTO
    {
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0.")]
        public decimal? Amount { get; set; }

        public string? Note { get; set; }
    }
}
