namespace Modelos.Request
{
    public class TemporadaCaricaturaRequest
    {
        public int TemporadaId { get; set; }
        public int CaricaturaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int? Capitulos { get; set; }
        public decimal? Calificacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Portada { get; set; } = string.Empty;
    }
}
