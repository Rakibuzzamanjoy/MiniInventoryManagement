using Microsoft.EntityFrameworkCore;
using MiniInventoryManagement.DAL.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniInventoryManagement.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }
        public DbSet<ProductInformation> ProductInformation { get; set; }
        public DbSet<OrderInformation> OrderInformation { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderHistory>()
                .HasOne<OrderInformation>()
                .WithMany(o => o.OrderItems)
                .HasForeignKey(h => h.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
