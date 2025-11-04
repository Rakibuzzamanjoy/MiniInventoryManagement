using Microsoft.AspNetCore.Mvc;
using MiniInventoryManagement.BLL.DTOs;
using MiniInventoryManagement.BLL.Services;

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
            var result = await _inventoryService.CreateNewOrder(orderInformation);
            return Ok(result);
        }
    }
}
