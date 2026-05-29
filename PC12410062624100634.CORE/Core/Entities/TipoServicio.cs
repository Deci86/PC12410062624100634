using System.ComponentModel.DataAnnotations;

namespace PC12410062624100634.CORE.Core.Entities
{
    public class TipoServicio
    {
        public TipoServicio()
        {
            OrdenServicios = new HashSet<OrdenServicio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PrecioBase { get; set; }

        // Propiedad de navegación: Un tipo de servicio puede estar en muchas órdenes
        public virtual ICollection<OrdenServicio> OrdenServicios { get; set; }
    }
}