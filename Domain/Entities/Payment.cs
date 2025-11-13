using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public partial class Payment : BaseFullEntity
{
    public int OrderId { get; set; }

    public int PaymentMethodId { get; set; }

    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public PaymentChannel Channel { get; set; } = PaymentChannel.Web;

    public DateTime? PaidDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
