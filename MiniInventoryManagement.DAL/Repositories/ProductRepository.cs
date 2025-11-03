using Microsoft.EntityFrameworkCore;
using MiniInventoryManagement.DAL.Context;
using MiniInventoryManagement.DAL.Models.DomainModels;
using MiniInventoryManagement.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniInventoryManagement.DAL.Repositories
{
    public interface IProductRepository
    {
        Task<ProductInformation> AddNewProduct(ProductInfoDTO productInformation);
        Task<List<ProductInformation>> ViewProductList();
        Task<ProductInformation> GetProductInfoById(int id);
        Task<ProductInformation> ModifyProductInfo(int id, ProductInfoDTO productInformation);
        Task<ProductInformation> RemoveProduct(int id);
        Task<ProductInformation> UpdateProductStock(int id, int quantity);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<ProductInformation> AddNewProduct(ProductInfoDTO productInformation)
        {
            var product = new ProductInformation();
            
            product.Name = productInformation.Name;
            product.Description = productInformation.Description;   
            product.Price = productInformation.Price; 
            product.StockQuantity = productInformation.StockQuantity;
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            _dbContext.ProductInformation.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;


        }

        public async Task<List<ProductInformation>> ViewProductList()
        {
            var result = await _dbContext.ProductInformation.ToListAsync();
            return result;
        }

        public async Task<ProductInformation> GetProductInfoById(int id)
        {
            var productInfo = await _dbContext.ProductInformation.FirstOrDefaultAsync(x => x.ProductId == id);
            if (productInfo == null)
            {
                return null;
            }
            return productInfo;
        }

        public async Task<ProductInformation> ModifyProductInfo(int id, ProductInfoDTO productInformation)
        {
            var productInfo = await GetProductInfoById(id);
            if (productInfo == null) 
            {
                return null;
            }
            else
            {
                productInfo.Name = productInformation.Name == null ? productInfo.Name : productInformation.Name;
                productInfo.Description = productInformation.Description == null ? productInfo.Description : productInformation.Description;
                productInfo.Price = productInformation.Price == 0 ? productInfo.Price : productInformation.Price;
                productInfo.StockQuantity = productInformation.StockQuantity == 0 ? productInfo.StockQuantity : productInformation.StockQuantity;
                productInfo.UpdatedAt = DateTime.Now;
            }

            await _dbContext.SaveChangesAsync();
            return productInfo;            
        }

        public async Task<ProductInformation> RemoveProduct(int id)
        {
            var result = await GetProductInfoById(id);

            if (result == null)
            {
                return null;
            }

           else
            {
                _dbContext.ProductInformation.Remove(result);
            }          
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<ProductInformation> UpdateProductStock(int id, int quantity)
        {
            var product = await GetProductInfoById(id);
            if (product != null)
            {
                product.StockQuantity = product.StockQuantity-quantity;
            }
            await _dbContext.SaveChangesAsync();
            return product;
        }

    }
}
