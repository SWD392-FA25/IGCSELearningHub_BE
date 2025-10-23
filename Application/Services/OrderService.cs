using Application.Services.Interfaces;
using Application.ViewModels.Orders;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IUnitOfWork uow, ILogger<OrderService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Result<OrderSummaryDTO>> CreateOrderAsync(int accountId, CreateOrderRequest req)
        {
            if (req.Items == null || req.Items.Count == 0)
                return Result<OrderSummaryDTO>.Fail("No items.", 400);

            // Validate & price lines
            var lines = new List<OrderDetail>();
            decimal total = 0m;

            foreach (var it in req.Items)
            {
                if (it.Quantity <= 0) return Result<OrderSummaryDTO>.Fail("Invalid quantity.", 400);

                switch (it.ItemType)
                {
                    case ItemType.Course:
                        {
                            var c = await _uow.CourseRepository.GetByIdAsync(it.ItemId);
                            if (c == null) return Result<OrderSummaryDTO>.Fail($"Course #{it.ItemId} not found.", 404);
                            var price = c.Price;
                            lines.Add(new OrderDetail { ItemType = ItemType.Course, ItemId = c.Id, Price = price, /*qty?*/ });
                            total += price * it.Quantity;
                            break;
                        }
                    case ItemType.CoursePackage:
                        {
                            var p = await _uow.CoursePackageRepository.GetByIdAsync(it.ItemId);
                            if (p == null) return Result<OrderSummaryDTO>.Fail($"Package #{it.ItemId} not found.", 404);
                            var price = p.Price;
                            lines.Add(new OrderDetail { ItemType = ItemType.CoursePackage, ItemId = p.Id, Price = price });
                            total += price * it.Quantity;
                            break;
                        }
                    case ItemType.Livestream:
                        {
                            var l = await _uow.LivestreamRepository.GetByIdAsync(it.ItemId);
                            if (l == null) return Result<OrderSummaryDTO>.Fail($"Livestream #{it.ItemId} not found.", 404);
                            var price = l.Price;
                            lines.Add(new OrderDetail { ItemType = ItemType.Livestream, ItemId = l.Id, Price = price });
                            total += price * it.Quantity;
                            break;
                        }
                    default:
                        return Result<OrderSummaryDTO>.Fail("Unsupported item type.", 400);
                }
            }

            var order = new Order
            {
                AccountId = accountId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = total,
                Status = OrderStatus.Pending
            };
            await _uow.OrderRepository.AddAsync(order);
            await _uow.SaveChangesAsync();

            foreach (var d in lines)
            {
                d.OrderId = order.Id;
                await _uow.OrderDetailRepository.AddAsync(d);
            }
            await _uow.SaveChangesAsync();

            // Build response
            var dto = await BuildOrderSummaryAsync(order.Id, accountId);
            return Result<OrderSummaryDTO>.Success(dto, "Order created.", 201);
        }

        public async Task<Result<OrderSummaryDTO>> GetOrderAsync(int accountId, int orderId)
        {
            var exists = await _uow.OrderRepository.GetAllQueryable()
                .AnyAsync(o => o.Id == orderId && o.AccountId == accountId);
            if (!exists) return Result<OrderSummaryDTO>.Fail("Order not found.", 404);

            var dto = await BuildOrderSummaryAsync(orderId, accountId);
            return Result<OrderSummaryDTO>.Success(dto);
        }

        public async Task<PagedResult<MyOrderListItemDTO>> GetMyOrdersAsync(int accountId, int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is <= 0 or > 100 ? 20 : pageSize;

            var q = _uow.OrderRepository.GetAllQueryable()
                .Where(o => o.AccountId == accountId);

            var total = await q.CountAsync();
            var data = await q.OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new MyOrderListItemDTO
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status.ToString(),
                    Lines = o.OrderDetails.Count(x => !x.IsDeleted)
                }).ToListAsync();

            return PagedResult<MyOrderListItemDTO>.Success(data, total, page, pageSize);
        }

        // -------- helper --------
        private async Task<OrderSummaryDTO> BuildOrderSummaryAsync(int orderId, int accountId)
        {
            var o = await _uow.OrderRepository.GetAllQueryable(
                    $"{nameof(Order.OrderDetails)}," +
                    $"{nameof(Order.OrderDetails)}.{nameof(OrderDetail.Order)}")
                .FirstAsync(x => x.Id == orderId && x.AccountId == accountId);

            // map titles by type
            var lines = new List<OrderDetailLineDTO>();
            foreach (var d in o.OrderDetails.Where(x => !x.IsDeleted))
            {
                string title;
                switch (d.ItemType)
                {
                    case ItemType.Course:
                        var c = await _uow.CourseRepository.GetByIdAsync(d.ItemId);
                        title = c?.Title ?? $"Course #{d.ItemId}";
                        break;
                    case ItemType.CoursePackage:
                        var p = await _uow.CoursePackageRepository.GetByIdAsync(d.ItemId);
                        title = p?.Name ?? $"Package #{d.ItemId}";
                        break;
                    case ItemType.Livestream:
                        var l = await _uow.LivestreamRepository.GetByIdAsync(d.ItemId);
                        title = l?.Title ?? $"Livestream #{d.ItemId}";
                        break;
                    default: title = $"Item #{d.ItemId}"; break;
                }

                lines.Add(new OrderDetailLineDTO
                {
                    OrderDetailId = d.Id,
                    ItemType = d.ItemType,
                    ItemId = d.ItemId,
                    Title = title,
                    UnitPrice = d.Price,
                    Quantity = 1,                 // nếu muốn lưu Quantity, thêm field vào OrderDetail
                    LineTotal = d.Price * 1
                });
            }

            return new OrderSummaryDTO
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                Items = lines
            };
        }
    }
}
