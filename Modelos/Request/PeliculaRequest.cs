namespace Modelos.Request
{
    public class PeliculaRequest
    {
        public int PeliculaId { get; set; }
        public int CaricaturaId { get; set; }
        public int SerieId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string OtrosNombres { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public DateTime? Estreno { get; set; }
        public DateTime? EstrenoMexico { get; set; }
        public decimal Calificacion { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public int ClasificacionId { get; set; }
        public string Productora { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public string Portada { get; set; } = string.Empty;
    }
}
