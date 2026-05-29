using System.Collections.Generic;
using PC12410062624100634.CORE.Core.DTOs;
using PC12410062624100634.CORE.Core.Entities;
using PC12410062624100634.CORE.Core.Interfaces;

namespace PC12410062624100634.CORE.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductListDTO>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            var productsDTOs = new List<ProductListDTO>();

            foreach (var product in products)
            {
                var dto = new ProductListDTO
                {
                    Id = product.Id,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price ?? 0,
                    Stock = product.Stock ?? 0
                };
                productsDTOs.Add(dto);
            }
            return productsDTOs;
        }

        public async Task<ProductListDTO> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
                return null;

            var dto = new ProductListDTO
            {
                Id = product.Id,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price ?? 0,
                Stock = product.Stock ?? 0
            };
            return dto;
        }

        public async Task CreateProduct(ProductCreateDTO productCreateDTO)
        {
            var product = new Product
            {
                Description = productCreateDTO.Description,
                ImageUrl = productCreateDTO.ImageUrl,
                Stock = productCreateDTO.Stock,
                Price = productCreateDTO.Price,
                Discount = productCreateDTO.Discount,
                CategoryId = productCreateDTO.CategoryId,
                IsActive = true
            };
            await _productRepository.CreateProduct(product);
        }

        public async Task UpdateProduct(ProductUpdateDTO productUpdateDTO)
        {
            var existing = await _productRepository.GetProductById(productUpdateDTO.Id);
            if (existing != null)
            {
                existing.Description = productUpdateDTO.Description;
                existing.ImageUrl = productUpdateDTO.ImageUrl;
                existing.Stock = productUpdateDTO.Stock;
                existing.Price = productUpdateDTO.Price;
                existing.Discount = productUpdateDTO.Discount;
                existing.CategoryId = productUpdateDTO.CategoryId;
                await _productRepository.UpdateProduct(existing);
            }
        }

        public async Task DeleteProduct(ProductDeleteDTO productDeleteDTO)
        {
            await _productRepository.DeleteProduct(productDeleteDTO.Id);
        }
    }
}
