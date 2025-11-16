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
        public async Task<IActionResult> OrderList()
        {
            var result = await _inventoryService.GetOrderList();
            return Ok(result);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderInfoDTO orderInformation)
        {
            var result = await _inventoryService.CreateNewOrder(orderInformation);
            if (result != null)
            {
                return Json(new { success = true, message = "Product Added Successfully!" });
            }
            return Json(new { success = false, message = "Product Add Failed!" });
        }
    }
}
