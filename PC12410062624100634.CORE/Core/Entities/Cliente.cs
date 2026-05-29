using System.ComponentModel.DataAnnotations;

namespace PC12410062624100634.CORE.Core.Entities
{
    public class Cliente
    {
        public Cliente()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }

        public int Id { get; set; }
        public string Paterno { get; set; } = null!;
        public string? Materno { get; set; } // Opcional
        public string Nombres { get; set; } = null!;
        public string? Correo { get; set; } // Opcional
        public string? Telefono { get; set; } // Opcional

        // Propiedad de navegación: Un cliente puede tener varios vehículos
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}