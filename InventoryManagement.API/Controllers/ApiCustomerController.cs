using Microsoft.AspNetCore.Mvc;
using MiniInventoryManagement.BLL.Services;
using MiniInventoryManagement.DAL.Models.DomainModels;
using MiniInventoryManagement.DAL.Models.DTOs;

namespace InventoryManagement.API.Controllers
{
    public class ApiCustomerController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public ApiCustomerController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("OrderList")]
        public async Task<IActionResult> GetOrderList()
        {
            var result = await _inventoryService.GetOrderList();
            return Ok(result);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateNewOrder([FromBody] OrderInfoDTO orderInformation)
        {
            decimal orderAmount = 0;
            ProductInformation productInfo = new ProductInformation();
            var productModify = new ProductInformation();
            foreach (var item in orderInformation.OrderItems)
            {
                productInfo = await _inventoryService.GetProductInfoById(item.ProductId);
                if (productInfo == null)
                {
                    return NotFound("No Product available!");
                }
                else if (productInfo.StockQuantity < item.Quantity)
                {
                    return BadRequest("Insufficient product!");
                }
                else
                {
                    orderAmount = orderAmount + (productInfo.Price * item.Quantity);
                }
            }
            var result = await _inventoryService.CreateNewOrder(orderInformation, orderAmount);
            foreach (var item in orderInformation.OrderItems)
            {
                var product = await _inventoryService.GetProductInfoById(item.ProductId);
                var orderHistory = await _inventoryService.AddOrderToOrderHistory(item, result.OrderId, product.Price);
              
                productModify =  await _inventoryService.UpdateProductStock(orderHistory.ProductId, orderHistory.Quantity);
            }
            if(productModify != null)
            {
                result.Status = "Completed";
                await _inventoryService.UpdateOrderStatus(result);
            }

            return Ok(result);
        }
    }
}
