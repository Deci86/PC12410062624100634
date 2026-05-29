using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PC12410062624100634.CORE.Core.DTOs;
using PC12410062624100634.CORE.Core.Interfaces;

namespace PC12410062624100634.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productCreateDTO)
        {
            if (productCreateDTO == null) return BadRequest();
            await _productService.CreateProduct(productCreateDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO productUpdateDTO)
        {
            if (productUpdateDTO == null) return BadRequest();
            var existing = await _productService.GetProductById(productUpdateDTO.Id);
            if (existing == null) return NotFound();
            await _productService.UpdateProduct(productUpdateDTO);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody] ProductDeleteDTO productDeleteDTO)
        {
            var existing = await _productService.GetProductById(productDeleteDTO.Id);
            if (existing == null) return NotFound();
            await _productService.DeleteProduct(productDeleteDTO);
            return NoContent();
        }
    }
}
