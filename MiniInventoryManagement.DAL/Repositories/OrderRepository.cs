using Microsoft.EntityFrameworkCore;
using MiniInventoryManagement.DAL.Context;
using MiniInventoryManagement.DAL.Models.DomainModels;
using MiniInventoryManagement.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniInventoryManagement.DAL.Repositories
{
    public interface IOrderRepository
    {       
        Task<OrderInformation> CreateNewOrder(OrderInfoDTO orderInformation, decimal orderAmount);
        Task<List<OrderInformation>> GetOrderList();
        Task<OrderHistory> AddOrderToOrderHistory(OrderHistoryDTO item, int orderId, decimal price);
        Task<OrderInformation> UpdateOrderStatus(OrderInformation result);
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderInformation> CreateNewOrder(OrderInfoDTO orderInformation, decimal orderAmount)
        {
            var orderInfo = new OrderInformation();
            
            orderInfo.CustomerName = orderInformation.CustomerName;
            orderInfo.OrderDate = DateTime.Now;
            orderInfo.TotalAmount = orderAmount;

            _dbContext.OrderInformation.Add(orderInfo);
            await _dbContext.SaveChangesAsync();
            return orderInfo;

        }

        public async Task<List<OrderInformation>> GetOrderList()
        {
            var result = await _dbContext.OrderInformation.ToListAsync();
            return result;
        }

        public async Task<OrderHistory> AddOrderToOrderHistory(OrderHistoryDTO item, int orderId, decimal price)
        {
            var orderHistory = new OrderHistory();
            orderHistory.OrderId = orderId;
            orderHistory.ProductId = item.ProductId;
            orderHistory.Quantity = item.Quantity;
            orderHistory.UnitPrice = price;

            _dbContext.OrderHistory.Add(orderHistory);
            await _dbContext.SaveChangesAsync();
            return orderHistory;
        }

        public async Task<OrderInformation> UpdateOrderStatus(OrderInformation result)
        {
            var data = await _dbContext.OrderInformation.FirstOrDefaultAsync(x => x.OrderId == result.OrderId);
            data.Status = result.Status;
            await _dbContext.SaveChangesAsync();
            return data;
        }
    }
}
