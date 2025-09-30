using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class OrderDetail : BaseFullEntity
{

    public int OrderId { get; set; }

    public ItemType ItemType { get; set; } = ItemType.Course;

    public int ItemId { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;
}
