using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Orders
{
    public class PaymentStatusDTO
    {
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;        // Pending | Paid | Refunded | Canceled
        public DateTime? PaidDate { get; set; }
        public string Method { get; set; } = "";           // VNPay...
    }
}
