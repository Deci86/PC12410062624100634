using Microsoft.EntityFrameworkCore;
using PC12410062624100634.CORE.Core.DTOs;
using PC12410062624100634.CORE.Core.Entities;
using PC12410062624100634.CORE.Core.Interfaces;
using PC12410062624100634.CORE.Infrastructure.Data;

namespace PC12410062624100634.CORE.Infrastructure.Repositories
{
    public class OrdenServicioRepository : IOrdenServicioRepository
    {
        private readonly TallerMecanicoContext _context;

        public OrdenServicioRepository(TallerMecanicoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdenServicioDto>> GetAllAsync()
        {
            return await _context.OrdenServicios
                .Select(o => new OrdenServicioDto
                {
                    Id = o.Id,
                    FechaIngreso = o.FechaIngreso,
                    DescripcionProblema = o.DescripcionProblema,
                    CostoEstimado = o.CostoEstimado,
                    Estado = o.Estado,
                    VehiculoId = o.VehiculoId,
                    TipoServicioId = o.TipoServicioId
                }).ToListAsync();
        }

        public async Task<OrdenServicioDto?> GetByIdAsync(int id)
        {
            var o = await _context.OrdenServicios.FindAsync(id);
            if (o == null) return null;

            return new OrdenServicioDto
            {
                Id = o.Id,
                FechaIngreso = o.FechaIngreso,
                DescripcionProblema = o.DescripcionProblema,
                CostoEstimado = o.CostoEstimado,
                Estado = o.Estado,
                VehiculoId = o.VehiculoId,
                TipoServicioId = o.TipoServicioId
            };
        }

        public async Task AddAsync(OrdenServicioDto dto)
        {
            var orden = new OrdenServicio
            {
                DescripcionProblema = dto.DescripcionProblema,
                CostoEstimado = dto.CostoEstimado,
                Estado = string.IsNullOrEmpty(dto.Estado) ? "Ingresado" : dto.Estado,
                VehiculoId = dto.VehiculoId,
                TipoServicioId = dto.TipoServicioId
            };

            _context.OrdenServicios.Add(orden);
            await _context.SaveChangesAsync();
            dto.Id = orden.Id; // Devuelve el ID generado por la base de datos
        }

        public async Task UpdateAsync(OrdenServicioDto dto)
        {
            var orden = await _context.OrdenServicios.FindAsync(dto.Id);
            if (orden != null)
            {
                orden.DescripcionProblema = dto.DescripcionProblema;
                orden.CostoEstimado = dto.CostoEstimado;
                orden.Estado = dto.Estado;
                orden.VehiculoId = dto.VehiculoId;
                orden.TipoServicioId = dto.TipoServicioId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var orden = await _context.OrdenServicios.FindAsync(id);
            if (orden != null)
            {
                _context.OrdenServicios.Remove(orden);
                await _context.SaveChangesAsync();
            }
        }
    }
}