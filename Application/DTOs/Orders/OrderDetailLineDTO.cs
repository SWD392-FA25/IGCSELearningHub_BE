using Domain.Enums;
namespace Application.DTOs.Orders
{
    public class OrderDetailLineDTO
    {
        public int OrderDetailId { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemId { get; set; }
        public string Title { get; set; } = null!; // Course/Package/Livestream title
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
