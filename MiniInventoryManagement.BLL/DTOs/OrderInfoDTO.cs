using MiniInventoryManagement.DAL.Models;

namespace MiniInventoryManagement.BLL.DTOs
{
    public class OrderInfoDTO
    {
       
        public string? CustomerName { get; set; }     


        public List<OrderHistoryDTO> OrderItems { get; set; } = new();
    }
}
