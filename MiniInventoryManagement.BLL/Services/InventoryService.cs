using Microsoft.EntityFrameworkCore.Infrastructure;
using MiniInventoryManagement.DAL.Models.DomainModels;
using MiniInventoryManagement.DAL.Models.DTOs;
using MiniInventoryManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniInventoryManagement.BLL.Services
{
    public interface IInventoryService
    {
        Task<ProductInformation> AddNewProduct(ProductInfoDTO productInformation);
        Task<ProductInformation> ModifyProductInfo(int id, ProductInfoDTO productInformation);
        Task<List<ProductInformation>> ViewProductList();
        Task<ProductInformation> GetProductInfoById(int id);
        Task<ProductInformation> RemoveProduct(int id);

        Task<List<OrderInformation>> GetOrderList();

        Task<OrderInformation> CreateNewOrder(OrderInfoDTO orderInformation, decimal orderAmount);
        Task<OrderHistory> AddOrderToOrderHistory(OrderHistoryDTO item, int orderId, decimal price);
        Task<OrderInformation> UpdateOrderStatus(OrderInformation result);

        Task<ProductInformation> UpdateProductStock(int id, int quantity);
    }
    public class InventoryService:IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public InventoryService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ProductInformation> AddNewProduct(ProductInfoDTO productInformation)
        {
            var result = await _productRepository.AddNewProduct(productInformation);
            return result;
        }

        public async Task<List<ProductInformation>> ViewProductList()
        {
            var result = await _productRepository.ViewProductList();
            return result;
        }

        public async Task<ProductInformation> GetProductInfoById(int id)
        {
            var productInfo = await _productRepository.GetProductInfoById(id);
            return productInfo;
        }

        public async Task<ProductInformation> ModifyProductInfo(int id, ProductInfoDTO productInformation)
        {
            var result = await _productRepository.ModifyProductInfo(id, productInformation);
            return result;
        }

        public async Task<ProductInformation> RemoveProduct(int id)
        {
            var result = await _productRepository.RemoveProduct(id);
            return result;
        }


        public async Task<List<OrderInformation>> GetOrderList()
        {
            return await _orderRepository.GetOrderList();
        }

        public async Task<OrderInformation> CreateNewOrder(OrderInfoDTO orderInformation, decimal orderAmount)
        {            
            var orderInfo = await _orderRepository.CreateNewOrder(orderInformation, orderAmount);          
            return orderInfo;
        }

        public async Task<OrderHistory> AddOrderToOrderHistory(OrderHistoryDTO item, int orderId, decimal price)
        {
            var orderHistory = await _orderRepository.AddOrderToOrderHistory(item, orderId, price);
            return orderHistory;
        }

        public async Task<ProductInformation> UpdateProductStock(int id, int quantity)
        {
            return await _productRepository.UpdateProductStock(id, quantity);
        }

        public async Task<OrderInformation> UpdateOrderStatus(OrderInformation result)
        {
            return await _orderRepository.UpdateOrderStatus(result);
        }
    }
}
