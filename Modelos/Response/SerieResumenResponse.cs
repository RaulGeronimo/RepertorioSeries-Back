using System.Xml.Serialization;

namespace Modelos.Response
{
    public class SeriesSiguiendoResponse : TemporadasSerieResponse
    {
        public int SerieSiguiendoId { get; set; }
        public DateTime InicioVisualizacion { get; set; }
        public DateTime? FinVisualizacion { get; set; }
        public string EstatusVisualizacion { get; set; } = string.Empty;
    }

    public class SeriesNoSiguiendoResponse : TemporadasSerieResponse
    {
        public int SerieProximoId { get; set; }
        public bool EsProximo { get; set; } = true;
    }

    public class SeriesProximoResponse : TemporadasSerieResponse
    {
        public int SerieProximoId { get; set; }
        public bool EsProximo { get; set; } = true;
    }

    [XmlRoot("Temporadas")]
    public class SeriesResumenListResponse<T>
    {
        [XmlElement("Temporada")]
        public List<T> Items { get; set; } = new();
    }
}
