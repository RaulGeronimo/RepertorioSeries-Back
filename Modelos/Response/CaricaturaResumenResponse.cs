using System.Xml.Serialization;

namespace Modelos.Response
{
    public class CaricaturasSiguiendoResponse : TemporadasCaricaturaResponse
    {
        public int CaricaturaSiguiendoId { get; set; }
        public DateTime InicioVisualizacion { get; set; }
        public DateTime? FinVisualizacion { get; set; }
        public string EstatusVisualizacion { get; set; } = string.Empty;
    }

    public class CaricaturasNoSiguiendoResponse : TemporadasCaricaturaResponse
    {
        public int CaricaturaProximoId { get; set; }
        public bool EsProximo { get; set; } = true;
    }

    public class CaricaturasProximoResponse : TemporadasCaricaturaResponse
    {
        public int CaricaturaProximoId { get; set; }
        public bool EsProximo { get; set; } = true;
    }

    [XmlRoot("Temporadas")]
    public class CaricaturasResumenListResponse<T>
    {
        [XmlElement("Temporada")]
        public List<T> Items { get; set; } = new();
    }
}
