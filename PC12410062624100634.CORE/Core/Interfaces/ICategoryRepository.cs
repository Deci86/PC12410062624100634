using PC12410062624100634.CORE.Core.Entities;

namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateCategory(Category category);
        Task DeleteCategory(int id);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task UpdateCategory(Category category);
    }
}