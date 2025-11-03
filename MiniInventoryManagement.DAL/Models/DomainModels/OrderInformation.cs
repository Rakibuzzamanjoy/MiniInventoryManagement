using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniInventoryManagement.DAL.Models.DomainModels
{
    public class OrderInformation
    {
        [Key]
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";

        public List<OrderHistory> OrderItems { get; set; } = new();
        
    }
    
}
