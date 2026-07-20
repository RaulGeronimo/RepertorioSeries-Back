using System.Xml.Serialization;

namespace Modelos.Response
{
    public class TemporadaSerieBaseResponse
    {
        public int TemporadaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Capitulos { get; set; }
        public decimal Calificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Portada { get; set; } = string.Empty;
    }

    public class TemporadasSerieResponse : TemporadaSerieBaseResponse
    {
        public string Serie { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Semanas { get; set; } = string.Empty;
        public int Anios { get; set; }
        public int TotalRegistros { get; set; }
    }

    public class TemporadaSerieResponse : TemporadaSerieBaseResponse
    {
        public int SerieId { get; set; }
    }

    [XmlRoot("Temporadas")]
    public class TemporadasSerieListResponse<T>
    {
        [XmlElement("Temporada")]
        public List<T> Items { get; set; } = new();
    }
}
