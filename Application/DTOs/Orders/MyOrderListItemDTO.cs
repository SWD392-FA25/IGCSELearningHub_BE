﻿
namespace Application.ViewModels.Orders
{
    public class MyOrderListItemDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public int Lines { get; set; }
    }
}
