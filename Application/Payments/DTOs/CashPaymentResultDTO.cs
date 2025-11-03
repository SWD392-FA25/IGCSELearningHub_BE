namespace Application.Payments.DTOs
{
    public class CashPaymentResultDTO
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime PaidDate { get; set; }
        public string Method { get; set; } = "Cash";
    }
}
