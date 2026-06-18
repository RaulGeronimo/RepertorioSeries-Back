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

    #region Obtener Id
    public class CaricaturaSiguiendoResponse
    {
        public int CaricaturaSiguiendoId { get; set; }
        public int UsuarioId { get; set; }
        public int TemporadaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class CaricaturaProximoResponse
    {
        public int CaricaturaProximoId { get; set; }
        public int UsuarioId { get; set; }
        public int TemporadaId { get; set; }
    }
    #endregion Obtener Id

    [XmlRoot("Temporadas")]
    public class CaricaturasResumenListResponse<T>
    {
        [XmlElement("Temporada")]
        public List<T> Items { get; set; } = new();
    }
}
