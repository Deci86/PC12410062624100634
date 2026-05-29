using Microsoft.EntityFrameworkCore;
using PC12410062624100634.CORE.Core.Entities;
using PC12410062624100634.CORE.Core.Interfaces;
using PC12410062624100634.CORE.Infrastructure.Data;

namespace PC12410062624100634.CORE.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Product.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            var existingProduct = await _context.Product.Where(p => p.Id == product.Id).FirstOrDefaultAsync();
            if (existingProduct != null)
            {
                existingProduct.Description = product.Description;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.Stock = product.Stock;
                existingProduct.Price = product.Price;
                existingProduct.Discount = product.Discount;
                existingProduct.CategoryId = product.CategoryId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(int id)
        {
            var existingProduct = await _context.Product.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (existingProduct != null)
            {
                existingProduct.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
