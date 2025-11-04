using Microsoft.EntityFrameworkCore;
using MiniInventoryManagement.DAL.Context;
using MiniInventoryManagement.DAL.Models;

namespace MiniInventoryManagement.DAL.Repositories
{
    public interface IProductRepository
    {
        Task<ProductInformation> AddNewProduct(ProductInformation productInformation);
        Task<List<ProductInformation>> ViewProductList();
        Task<ProductInformation> GetProductInfoById(int id);
        Task<ProductInformation> ModifyProductInfo(int id, ProductInformation productInformation);
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

        public async Task<ProductInformation> AddNewProduct(ProductInformation productInformation)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Error adding new product: " + ex.Message);
            }

        }

        public async Task<List<ProductInformation>> ViewProductList()
        {
            var result = await _dbContext.ProductInformation.ToListAsync();
            return result;
        }

        public async Task<ProductInformation> GetProductInfoById(int id)
        {
            var productInfo = await _dbContext.ProductInformation.FirstOrDefaultAsync(x => x.ProductId == id);
            if (productInfo != null)
            {
                return productInfo;
            }
            return null;
        }

        public async Task<ProductInformation> ModifyProductInfo(int id, ProductInformation productInformation)
        {
            try
            {
                var productInfo = await GetProductInfoById(id);

                if (productInfo != null)
                {
                    productInfo.Name = productInformation.Name == null || productInformation.Name == "" ? productInfo.Name : productInformation.Name;
                    productInfo.Description = productInformation.Description == null || productInformation.Description == "" ? productInfo.Description : productInformation.Description;
                    productInfo.Price = productInformation.Price == 0 ? productInfo.Price : productInformation.Price;
                    productInfo.StockQuantity = productInformation.StockQuantity == 0 ? productInfo.StockQuantity : productInformation.StockQuantity;
                    productInfo.UpdatedAt = DateTime.Now;

                    await _dbContext.SaveChangesAsync();
                    return productInfo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error modifying product information: " + ex.Message);
            }
        }

        public async Task<ProductInformation> RemoveProduct(int id)
        {
            var result = await GetProductInfoById(id);

            if (result != null)            
            {
                _dbContext.ProductInformation.Remove(result);

                await _dbContext.SaveChangesAsync();
                return result;
            }          
            
            else
            {
                return null;
            }

           
        }

        public async Task<ProductInformation> UpdateProductStock(int id, int quantity)
        {
            try
            {
                var product = await GetProductInfoById(id);
                if (product != null)
                {
                    product.StockQuantity = product.StockQuantity - quantity;

                    await _dbContext.SaveChangesAsync();
                    return product;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product stock: " + ex.Message);
            }

        }

    }
}
