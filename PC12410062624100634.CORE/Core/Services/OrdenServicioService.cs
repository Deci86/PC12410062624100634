using PC12410062624100634.CORE.Core.DTOs;
using PC12410062624100634.CORE.Core.Interfaces;

namespace PC12410062624100634.CORE.Core.Services
{
    public class OrdenServicioService : IOrdenServicioService
    {
        private readonly IOrdenServicioRepository _repository;

        public OrdenServicioService(IOrdenServicioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrdenServicioDto>> GetTodasLasOrdenesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OrdenServicioDto?> ObtenerOrdenPorIdAsync(int id)
        {
            if (id <= 0) return null;
            return await _repository.GetByIdAsync(id);
        }

        public async Task<OrdenServicioDto> RegistrarOrdenAsync(OrdenServicioDto dto)
        {
            // Regla de negocio elemental: El costo estimado base no puede ser negativo
            if (dto.CostoEstimado < 0)
                throw new ArgumentException("El costo estimado de la orden no puede ser menor a cero.");

            // El estado por defecto inicial siempre es 'Ingresado'
            if (string.IsNullOrEmpty(dto.Estado))
            {
                dto.Estado = "Ingresado";
            }

            dto.FechaIngreso = DateTime.Now;

            await _repository.AddAsync(dto);
            return dto;
        }

        public async Task<bool> ActualizarOrdenAsync(OrdenServicioDto dto)
        {
            var ordenExistente = await _repository.GetByIdAsync(dto.Id);
            if (ordenExistente == null) return false;

            await _repository.UpdateAsync(dto);
            return true;
        }

        public async Task<bool> EliminarOrdenAsync(int id)
        {
            var ordenExistente = await _repository.GetByIdAsync(id);
            if (ordenExistente == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}