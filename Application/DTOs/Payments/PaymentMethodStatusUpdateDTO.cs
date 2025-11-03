using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Payments
{
    public class PaymentMethodStatusUpdateDTO
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
