using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Orders
{
    public class OrderItemRequest
    {
        public ItemType ItemType { get; set; }  // Course | CoursePackage | Livestream
        public int ItemId { get; set; }
        public int Quantity { get; set; } = 1;  // giữ để mở rộng; hiện giá theo item, không nhân qty nếu muốn
    }
}
