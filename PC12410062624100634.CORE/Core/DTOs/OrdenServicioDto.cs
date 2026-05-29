namespace PC12410062624100634.CORE.Core.DTOs
{
    public class OrdenServicioDto
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string DescripcionProblema { get; set; } = null!;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = null!;
        public int VehiculoId { get; set; }
        public int TipoServicioId { get; set; }
    }
}