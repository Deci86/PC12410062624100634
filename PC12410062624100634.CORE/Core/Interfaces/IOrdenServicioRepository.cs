using PC12410062624100634.CORE.Core.DTOs;

namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface IOrdenServicioRepository
    {
        Task<IEnumerable<OrdenServicioDto>> GetAllAsync();
        Task<OrdenServicioDto?> GetByIdAsync(int id);
        Task AddAsync(OrdenServicioDto dto);
        Task UpdateAsync(OrdenServicioDto dto);
        Task DeleteAsync(int id);
    }
}