using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC12410062624100634.CORE.Core.Entities; // Asegúrate de que este sea el namespace de tus entidades
using PC12410062624100634.CORE.Infrastructure.Data; // Asegúrate de que este sea el namespace de tu DbContext

namespace PC12410062624100634.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicioController : ControllerBase
    {
        // 1. Inyección directa del DbContext (Sin patrón Repository)
        private readonly TallerMecanicoContext _context;

        public TipoServicioController(TallerMecanicoContext context)
        {
            _context = context;
        }

        // 2. MÉTODO GET: Listar todos los tipos de servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServicio>>> Get()
        {
            var servicios = await _context.TipoServicios.ToListAsync();
            return Ok(servicios);
        }

        // 3. MÉTODO GET (Por ID): Obtener un tipo de servicio específico
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoServicio>> GetById(int id)
        {
            var tipoServicio = await _context.TipoServicios.FindAsync(id);
            if (tipoServicio == null)
            {
                return NotFound($"No se encontró el tipo de servicio con ID {id}");
            }
            return Ok(tipoServicio);
        }

        // 4. MÉTODO POST: Crear un nuevo tipo de servicio
        [HttpPost]
        public async Task<ActionResult<TipoServicio>> Post([FromBody] TipoServicio tipoServicio)
        {
            if (tipoServicio == null)
            {
                return BadRequest("Los datos del servicio son requeridos.");
            }

            _context.TipoServicios.Add(tipoServicio);
            await _context.SaveChangesAsync();

            // Devuelve un estado 201 Created y la ruta para consultar el nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = tipoServicio.Id }, tipoServicio);
        }

        // 5. MÉTODO PUT: Actualizar un tipo de servicio existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TipoServicio tipoServicio)
        {
            if (tipoServicio == null || id != tipoServicio.Id)
            {
                return BadRequest("El ID del parámetro no coincide con el ID del cuerpo.");
            }

            // Marcamos la entidad como modificada para que EF genere el UPDATE
            _context.Entry(tipoServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoServicioExists(id))
                {
                    return NotFound($"El tipo de servicio con ID {id} ya no existe.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Código 204 estándar para actualizaciones exitosas sin contenido de retorno
        }

        // 6. MÉTODO DELETE: Eliminar un tipo de servicio
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoServicio = await _context.TipoServicios.FindAsync(id);
            if (tipoServicio == null)
            {
                return NotFound($"No se encontró el tipo de servicio con ID {id} para eliminar.");
            }

            _context.TipoServicios.Remove(tipoServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar interno para validar la existencia del registro
        private bool TipoServicioExists(int id)
        {
            return _context.TipoServicios.Any(e => e.Id == id);
        }
    }
}