using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Orders
{
    public class CreateOrderRequest
    {
        public List<OrderItemRequest> Items { get; set; } = new();
    }
}
