using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PC12410062624100634.CORE.Core.DTOs;
using PC12410062624100634.CORE.Core.Entities;
using PC12410062624100634.CORE.Core.Interfaces;

namespace PC12410062624100634.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (categoryCreateDTO == null)
            {
                return BadRequest();
            }

            await _categoryService.CreateCategory(categoryCreateDTO);
            return Ok();
        }

        //HttpPut api/category
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            if (categoryUpdateDTO == null)
            {
                return BadRequest();
            }
            var existingCategory = await _categoryService.GetCategoryById(categoryUpdateDTO.Id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            await _categoryService.UpdateCategory(categoryUpdateDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromBody] CategoryDeleteDTO categoryDeleteDTO)
        {
            var existingCategory = await _categoryService.GetCategoryById(categoryDeleteDTO.Id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteCategory(categoryDeleteDTO);
            return NoContent();
        }
    }

}
