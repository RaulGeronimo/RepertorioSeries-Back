using System.Xml.Serialization;

namespace Modelos.Response
{
    public class CaricaturaBaseResponse
    {
        public int CaricaturaId { get; set; }
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

    public class CaricaturasResponse : CaricaturaBaseResponse
    {
        public int Temporadas { get; set; }
        public int Peliculas { get; set; }
        public int Capitulos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal Promedio { get; set; }
        public int TotalRegistros { get; set; }
    }

    public class CaricaturaResponse : CaricaturaBaseResponse { }

    [XmlRoot("Caricaturas")]
    public class CaricaturasListResponse<T>
    {
        [XmlElement("Caricatura")]
        public List<T> Items { get; set; } = new();
    }
}
