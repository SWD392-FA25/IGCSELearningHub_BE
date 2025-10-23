using Domain.Common;

namespace Domain.Entities;

public partial class PaymentMethod : BaseFullEntity
{
    public string PaymentMethodName { get; set; } = null!;

    public string? PaymentMethodDescription { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
