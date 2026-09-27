using System.Xml.Serialization;

namespace Modelos.Response
{
    public class PeliculasResponse
    {
        public int PeliculaId { get; set; }
        public string Universo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string OtrosNombres { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public DateTime Estreno { get; set; }
        public DateTime EstrenoMexico { get; set; }
        public decimal Calificacion { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public string Clasificacion { get; set; } = string.Empty;
        public string Productora { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public string Portada { get; set; } = string.Empty;
        public int TotalRegistros { get; set; }
    }

    [XmlRoot("Peliculas")]
    public class PeliculasListResponse<T>
    {
        [XmlElement("Pelicula")]
        public List<T> Items { get; set; } = new();
    }
}
