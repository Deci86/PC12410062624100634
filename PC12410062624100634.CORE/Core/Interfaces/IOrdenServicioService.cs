using PC12410062624100634.CORE.Core.DTOs;

namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface IOrdenServicioService
    {
        Task<IEnumerable<OrdenServicioDto>> GetTodasLasOrdenesAsync();
        Task<OrdenServicioDto?> ObtenerOrdenPorIdAsync(int id);
        Task<OrdenServicioDto> RegistrarOrdenAsync(OrdenServicioDto dto);
        Task<bool> ActualizarOrdenAsync(OrdenServicioDto dto);
        Task<bool> EliminarOrdenAsync(int id);
    }
}