using PC12410062624100634.CORE.Core.DTOs;

namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryCreateDTO categoryCreateDTO);
        Task DeleteCategory(CategoryDeleteDTO categoryDeleteDTO);
        Task<IEnumerable<CategoryListDTO>> GetCategories();
        Task<CategoryListDTO> GetCategoryById(int id);
        Task UpdateCategory(CategoryUpdateDTO categoryUpdateDTO);
    }
}