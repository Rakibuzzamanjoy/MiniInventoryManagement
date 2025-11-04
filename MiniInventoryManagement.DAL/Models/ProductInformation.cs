using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniInventoryManagement.DAL.Models
{
    public class ProductInformation
    {
        [Key]
        public int ProductId {  get; set; }

        [Required]
        [StringLength(100)]
        public string Name {  get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description {  get; set; }

        [Required]
        public decimal Price {  get; set; }

        [Required]
        public int StockQuantity {  get; set; }
        public DateTime CreatedAt {  get; set; } = DateTime.Now;
        public DateTime UpdatedAt {  get; set; } = DateTime.Now;
    }
}
