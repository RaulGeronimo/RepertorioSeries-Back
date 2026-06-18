namespace Modelos.Request
{
    public class BitacoraCargaRequest
    {
        public string Tabla { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int UsuarioId { get; set; } = 0;
        public DateTime Modificado { get; set; } = DateTime.Now;
        public string Proceso { get; set; } = string.Empty;
        public int RegistroId { get; set; } = 0;
    }

    public class BuscarBitacoraCargaRequest
    {
        public string Tabla { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public string Proceso { get; set; } = string.Empty;
    }
}
