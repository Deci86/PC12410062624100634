using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PC12410062624100634.CORE.Core.Entities;
using PC12410062624100634.CORE.Core.Interfaces;
using PC12410062624100634.CORE.Infrastructure.Data;

namespace PC12410062624100634.CORE.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Category.ToListAsync();
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Category.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
        //Create Ccategory
        public async Task CreateCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
        }
        //Update Category
        public async Task UpdateCategory(Category category)
        {
            var existingCategory = await
                                _context
                                .Category
                                .Where(c => c.Id == category.Id)
                                .FirstOrDefaultAsync();
            if (existingCategory != null)
            {
                existingCategory.Description = category.Description;
                await _context.SaveChangesAsync();
            }
        }
        //Delete Category
        public async Task DeleteCategory(int id)
        {
            var existingCategory = await _context
                                .Category
                                .Where(c => c.Id == id)
                                .FirstOrDefaultAsync();
            if (existingCategory != null)
            {
                existingCategory.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
