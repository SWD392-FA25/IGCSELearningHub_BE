using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Orders
{
    public class OrderStatusDTO
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = null!;        // Pending | Paid | Canceled | ...
        public decimal TotalAmount { get; set; }
        public PaymentStatusDTO? LastPayment { get; set; }
    }
}
