using System.ComponentModel.DataAnnotations;

namespace PC12410062624100634.CORE.Core.Entities
{
    public class Vehiculo
    {
        public Vehiculo()
        {
            OrdenServicios = new HashSet<OrdenServicio>();
        }

        public int Id { get; set; }
        public string Placa { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int ClienteId { get; set; }

        // Propiedades de navegación
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual ICollection<OrdenServicio> OrdenServicios { get; set; }
    }
}