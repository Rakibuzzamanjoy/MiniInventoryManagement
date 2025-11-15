using AutoMapper;
using MiniInventoryManagement.BLL.DTOs;
using MiniInventoryManagement.DAL.Models;
using MiniInventoryManagement.DAL.Repositories;


namespace MiniInventoryManagement.BLL.Services
{
    public interface IInventoryService
    {
        Task<ProductInformation> AddNewProduct(ProductInfoDTO productInformation);
        Task<ProductInformation> ModifyProductInfo(int id, ProductInfoDTO productInformation);
        Task<List<ProductInformation>> ViewProductList();       
        Task<ProductInformation> RemoveProduct(int id);

        Task<List<OrderInformation>> GetOrderList();

        Task<OrderInformation> CreateNewOrder(OrderInfoDTO orderInformation);
        Task<ProductInformation> GetProductInfoById(int id);


    }
    public class InventoryService:IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public InventoryService(IProductRepository productRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ProductInformation> AddNewProduct(ProductInfoDTO productInformation)
        {
            var productInfo = _mapper.Map<ProductInformation>(productInformation);
            var result = await _productRepository.AddNewProduct(productInfo);            
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
            var mapData = _mapper.Map<ProductInformation>(productInformation);
            var result = await _productRepository.ModifyProductInfo(id, mapData);           
            return result;
        }

        public async Task<ProductInformation> RemoveProduct(int id)
        {
            var result = await _productRepository.RemoveProduct(id);            
            return result;
        }
        
        public async Task<List<OrderInformation>> GetOrderList()
        {
            var data = await _orderRepository.GetOrderList();          
            return data;
        }

        public async Task<OrderInformation> CreateNewOrder(OrderInfoDTO orderInformation)
        {
            decimal orderAmount = 0;
            ProductInformation productInfo = new ProductInformation();
            var productModify = new ProductInformation();
            foreach (var item in orderInformation.OrderItems)
            {
                productInfo = await _productRepository.GetProductInfoById(item.ProductId);
                if (productInfo == null)
                {
                    throw new Exception ("No Product available!");
                }
                else if (productInfo.StockQuantity < item.Quantity)
                {
                    throw new Exception ("Insufficient product!");
                }
                else
                {
                    orderAmount = orderAmount + (productInfo.Price * item.Quantity);
                }
            }
            var orderInfo = _mapper.Map<OrderInformation>(orderInformation);
            var result = await _orderRepository.CreateNewOrder(orderInfo, orderAmount);
            foreach (var item in orderInformation.OrderItems)
            {
                var product = await _productRepository.GetProductInfoById(item.ProductId);

                var historyMapData = _mapper.Map<OrderHistory>(item);
                var orderHistory = await _orderRepository.AddOrderToOrderHistory(historyMapData, result.OrderId, product.Price);

                productModify = await _productRepository.UpdateProductStock(orderHistory.ProductId, orderHistory.Quantity);
            }
            if (productModify != null)
            {
                result.Status = "Completed";
                await _orderRepository.UpdateOrderStatus(result);
            }            
           
            return result;
        }

        public async Task<OrderHistory> AddOrderToOrderHistory(OrderHistoryDTO item, int orderId, decimal price)
        {
            var mapData = _mapper.Map<OrderHistory>(item);
            var orderHistory = await _orderRepository.AddOrderToOrderHistory(mapData, orderId, price);         
            return orderHistory;
        }

        public async Task<ProductInformation> UpdateProductStock(int id, int quantity)
        {
            var res = await _productRepository.UpdateProductStock(id, quantity);        
            return res;
        }

        public async Task<OrderInformation> UpdateOrderStatus(OrderInfoDTO result)
        {
            var mapData = _mapper.Map<OrderInformation>(result);
            var res = await _orderRepository.UpdateOrderStatus(mapData);           
            return res;
        }
    }
}
