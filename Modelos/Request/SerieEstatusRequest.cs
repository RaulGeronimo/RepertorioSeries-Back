namespace Modelos.Request
{
    public class SerieSiguiendoRequest
    {
        public int SerieSiguiendoId { get; set; }
        public int UsuarioId { get; set; }
        public int TemporadaId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class SerieProximoRequest
    {
        public int SerieProximoId { get; set; }
        public int UsuarioId { get; set; }
        public int TemporadaId { get; set; }
    }
}
