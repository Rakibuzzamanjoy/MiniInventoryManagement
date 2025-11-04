using Microsoft.EntityFrameworkCore;
using MiniInventoryManagement.DAL.Context;
using MiniInventoryManagement.DAL.Models;

namespace MiniInventoryManagement.DAL.Repositories
{
    public interface IOrderRepository
    {       
        Task<OrderInformation> CreateNewOrder(OrderInformation orderInformation, decimal orderAmount);
        Task<List<OrderInformation>> GetOrderList();
        Task<OrderHistory> AddOrderToOrderHistory(OrderHistory item, int orderId, decimal price);
        Task<OrderInformation> UpdateOrderStatus(OrderInformation result);
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderInformation> CreateNewOrder(OrderInformation orderInformation, decimal orderAmount)
        {
            try
            {
                var orderInfo = new OrderInformation();

                orderInfo.CustomerName = orderInformation.CustomerName;
                orderInfo.OrderDate = DateTime.Now;
                orderInfo.TotalAmount = orderAmount;

                _dbContext.OrderInformation.Add(orderInfo);
                await _dbContext.SaveChangesAsync();
                return orderInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating new order: " + ex.Message);
            }

        }

        public async Task<List<OrderInformation>> GetOrderList()
        {
            var result = await _dbContext.OrderInformation.ToListAsync();
            return result;
        }

        public async Task<OrderHistory> AddOrderToOrderHistory(OrderHistory item, int orderId, decimal price)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Error adding order to order history: " + ex.Message);
            }
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
