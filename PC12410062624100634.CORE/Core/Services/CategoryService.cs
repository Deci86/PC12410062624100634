using System;
using System.Collections.Generic;
using System.Text;
using PC12410062624100634.CORE.Core.DTOs;
using PC12410062624100634.CORE.Core.Entities;
using PC12410062624100634.CORE.Core.Interfaces;

namespace PC12410062624100634.CORE.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryListDTO>> GetCategories()
        {

            var categories = await _categoryRepository.GetCategories();
            var categoriesDTOs = new List<CategoryListDTO>();

            foreach (var category in categories)
            {
                var categoryDTO = new CategoryListDTO
                {
                    Id = category.Id,
                    Description = category.Description
                };
                categoriesDTOs.Add(categoryDTO);
            }
            return categoriesDTOs;
        }

        public async Task<CategoryListDTO> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return null;
            }
            var categoryDTO = new CategoryListDTO
            {
                Id = category.Id,
                Description = category.Description
            };
            return categoryDTO;
        }

        public async Task CreateCategory(CategoryCreateDTO categoryCreateDTO)
        {
            var category = new Category
            {
                Description = categoryCreateDTO.Description,
                IsActive = true
            };
            await _categoryRepository.CreateCategory(category);
        }

        public async Task UpdateCategory(CategoryUpdateDTO categoryUpdateDTO)
        {
            var existingCategory = await _categoryRepository.GetCategoryById(categoryUpdateDTO.Id);
            if (existingCategory != null)
            {
                existingCategory.Description = categoryUpdateDTO.Description;
                await _categoryRepository.UpdateCategory(existingCategory);
            }
        }

        public async Task DeleteCategory(CategoryDeleteDTO categoryDeleteDTO)
        {
            await _categoryRepository.DeleteCategory(categoryDeleteDTO.Id);
        }

    }
}
