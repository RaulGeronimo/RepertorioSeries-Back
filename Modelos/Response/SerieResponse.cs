using System.Xml.Serialization;

namespace Modelos.Response
{
    public class SeriesResponse
    {
        public int SerieId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string OtrosNombres { get; set; } = string.Empty;
        public int Peliculas { get; set; }
        public int Temporadas { get; set; }
        public int Capitulos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal Promedio { get; set; }
        public string Creador { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public string Difusion { get; set; } = string.Empty;
        public string Productora { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public string Portada { get; set; } = string.Empty;
        public int TotalRegistros { get; set; }
    }

    [XmlRoot("Series")]
    public class SeriesListResponse<T>
    {
        [XmlElement("Serie")]
        public List<T> Items { get; set; } = new();
    }
}
