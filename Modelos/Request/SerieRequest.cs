namespace Modelos.Request
{
    public class SerieRequest
    {
        public int SerieId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string OtrosNombres { get; set; } = string.Empty;
        public string Creador { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Productora { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public string Difusion { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public string Portada { get; set; } = string.Empty;
    }
}
