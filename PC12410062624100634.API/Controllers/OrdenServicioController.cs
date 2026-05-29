using Microsoft.AspNetCore.Mvc;
using PC12410062624100634.CORE.Core.DTOs;
using PC12410062624100634.CORE.Core.Interfaces;

namespace PC12410062624100634.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly IOrdenServicioService _service;

        public OrdenServicioController(IOrdenServicioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetTodasLasOrdenesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orden = await _service.ObtenerOrdenPorIdAsync(id);
            if (orden == null) return NotFound($"No se encontró la orden con ID {id}.");
            return Ok(orden);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrdenServicioDto dto)
        {
            try
            {
                var nuevaOrden = await _service.RegistrarOrdenAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = nuevaOrden.Id }, nuevaOrden);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrdenServicioDto dto)
        {
            if (id != dto.Id) return BadRequest("Inconsistencia en el ID.");

            var actualizado = await _service.ActualizarOrdenAsync(dto);
            if (!actualizado) return NotFound($"No se pudo actualizar. No existe la orden con ID {id}.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.EliminarOrdenAsync(id);
            if (!eliminado) return NotFound($"No se encontró la orden con ID {id} para eliminar.");

            return NoContent();
        }
    }
}