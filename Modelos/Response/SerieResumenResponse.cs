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

    #region Obtener Id
    public class SerieSiguiendoResponse
    {
        public int SerieSiguiendoId { get; set; }
        public int UsuarioId { get; set; }
        public int TemporadaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class SerieProximoResponse
    {
        public int SerieProximoId { get; set; }
        public int UsuarioId { get; set; }
        public int TemporadaId { get; set; }
    }
    #endregion Obtener Id

    [XmlRoot("Temporadas")]
    public class SeriesResumenListResponse<T>
    {
        [XmlElement("Temporada")]
        public List<T> Items { get; set; } = new();
    }
}
