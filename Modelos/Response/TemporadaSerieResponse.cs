using System.Xml.Serialization;

namespace Modelos.Response
{
    public class TemporadasSerieResponse
    {
        public int TemporadaId { get; set; }
        public string Serie { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Capitulos { get; set; }
        public decimal Calificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Semanas { get; set; } = string.Empty;
        public int Anios { get; set; }
        public string Portada { get; set; } = string.Empty;
        public int TotalRegistros { get; set; }
    }

    [XmlRoot("Temporadas")]
    public class TemporadasSerieListResponse<T>
    {
        [XmlElement("Temporada")]
        public List<T> Items { get; set; } = new();
    }
}
