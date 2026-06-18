using System.Xml.Serialization;

namespace Modelos.Response
{
    public class TemporadaCaricaturaBaseResponse
    {
        public int TemporadaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Capitulos { get; set; }
        public decimal Calificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Portada { get; set; } = string.Empty;
    }

    public class TemporadasCaricaturaResponse : TemporadaCaricaturaBaseResponse
    {
        public string Caricatura { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int Semanas { get; set; }
        public int Anios { get; set; }
        public int TotalRegistros { get; set; }
    }

    public class TemporadaCaricaturaResponse : TemporadaCaricaturaBaseResponse
    {
        public int CaricaturaId { get; set; }
    }

    [XmlRoot("Temporadas")]
    public class TemporadasCaricaturaListResponse<T>
    {
        [XmlElement("Temporada")]
        public List<T> Items { get; set; } = new();
    }
}
