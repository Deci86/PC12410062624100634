using PC12410062624100634.CORE.Core.DTOs;

namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(ProductCreateDTO productCreateDTO);
        Task DeleteProduct(ProductDeleteDTO productDeleteDTO);
        Task<IEnumerable<ProductListDTO>> GetProducts();
        Task<ProductListDTO> GetProductById(int id);
        Task UpdateProduct(ProductUpdateDTO productUpdateDTO);
    }
}
