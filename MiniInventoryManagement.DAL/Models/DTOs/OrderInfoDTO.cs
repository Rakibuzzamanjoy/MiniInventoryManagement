using MiniInventoryManagement.DAL.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniInventoryManagement.DAL.Models.DTOs
{
    public class OrderInfoDTO
    {
        public string? CustomerName { get; set; }

        public List<OrderHistoryDTO> OrderItems { get; set; } = new();
    }
}
