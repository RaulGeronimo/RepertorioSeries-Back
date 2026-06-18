namespace Modelos.Request
{
    public class BitacoraErrorRequest
    {
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int UsuarioId { get; set; } = 0;
        public string Code { get; set; } = string.Empty;
    }

    public class BuscarBitacoraErrorRequest
    {
        public string Tabla { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string Columna { get; set; } = string.Empty;
    }
}
