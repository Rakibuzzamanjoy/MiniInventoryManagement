using Microsoft.AspNetCore.Mvc;
using MiniInventoryManagement.BLL.Services;
using MiniInventoryManagement.DAL.Models.DomainModels;
using MiniInventoryManagement.DAL.Models.DTOs;

namespace InventoryManagement.API.Controllers
{
    public class ApiAdminController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public ApiAdminController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
         
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductInfoDTO productInformation)
        {
            var result = await _inventoryService.AddNewProduct(productInformation);
            return Ok(result);
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProductList()
        {
            var result = await _inventoryService.ViewProductList();
            return Ok(result);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult>UpdateProduct(int id, [FromBody] ProductInfoDTO productInformation)
        {
            
            
            var result = await _inventoryService.ModifyProductInfo(id, productInformation);
            if(result == null)
            {
                return NotFound("Product not available!");
            }
            return Ok(result);
        }

        [HttpDelete("RemoveProduct/{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var result = await _inventoryService.RemoveProduct(id);
            if (result == null)
            {
                return NotFound("Product not available!");
            }
            return Ok(result);
        }

    }
}
