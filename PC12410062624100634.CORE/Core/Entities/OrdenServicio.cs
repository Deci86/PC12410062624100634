using System.ComponentModel.DataAnnotations;

namespace PC12410062624100634.CORE.Core.Entities
{
    public class OrdenServicio
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string DescripcionProblema { get; set; } = null!;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = null!;
        public int VehiculoId { get; set; }
        public int TipoServicioId { get; set; }

        // Propiedades de navegación hacia los objetos relacionados
        public virtual Vehiculo Vehiculo { get; set; } = null!;
        public virtual TipoServicio TipoServicio { get; set; } = null!;
    }
}