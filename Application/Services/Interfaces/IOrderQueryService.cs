using Application.DTOs.Orders;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IOrderQueryService
    {
        Task<Result<OrderStatusDTO>> GetOrderStatusAsync(int accountId, int orderId);
    }
}
