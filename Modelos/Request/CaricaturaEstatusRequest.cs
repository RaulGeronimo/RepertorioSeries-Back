namespace Modelos.Request
{
    public class CaricaturaSiguiendoRequest
    {
        public int CaricaturaSiguiendoId { get; set; }
        public int UsuarioId { get; set; }
        public int TemporadaId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class CaricaturaProximoRequest
    {
        public int CaricaturaProximoId { get; set; }
        public int UsuarioId { get; set; }
        public int TemporadaId { get; set; }
    }
}
