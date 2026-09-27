using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Request;
using Modelos.Response;
using Negocio;

namespace SeriesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SerieSiguiendoController : Controller
    {
        private readonly DynamicService _service;
        GenericResponse response = new GenericResponse();

        public SerieSiguiendoController(DynamicService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] SerieSiguiendoRequest request)
        {
            request.UsuarioId = _service.ResolverUsuarioId();
            response = await _service.EjecutarSPConXml("scGuardarSerieSiguiendo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            return Ok(response);
        }

        [HttpPut("{SerieSiguiendoId}")]
        public async Task<IActionResult> Actualizar(int SerieSiguiendoId, [FromBody] SerieSiguiendoRequest request)
        {
            request.UsuarioId = _service.ResolverUsuarioId();
            request.SerieSiguiendoId = SerieSiguiendoId;
            response = await _service.EjecutarSPConXml("scActualizarSerieSiguiendo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            return Ok(response);
        }

        [HttpDelete("{SerieSiguiendoId}")]
        public async Task<IActionResult> Eliminar(int SerieSiguiendoId)
        {
            SerieSiguiendoRequest request = new SerieSiguiendoRequest { SerieSiguiendoId = SerieSiguiendoId };
            response = await _service.EjecutarSPConXml("scEliminarSerieSiguiendo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int id = int.TryParse(response.Result?["Id"]?.InnerText, out var parsedId) ? parsedId : 0;
            string nombre = response.Result?["Nombre"]?.InnerText!;
            response.Result = null;

            return Ok(response);
        }

        [HttpGet("Vistos")]
        public async Task<IActionResult> ObtenerVistos()
        {
            int UsuarioId = _service.ResolverUsuarioId();
            var (response, item) = await _service.EjecutarSPPorIdLista<SeriesResumenListResponse<SeriesSiguiendoResponse>, SeriesSiguiendoResponse>("scObtenerSerieSiguiendo", "@UsuarioId", UsuarioId);

            if (!response.Success)
            { return BadRequest(response); }


            return Ok(item);
        }

        [HttpGet("NoVistos")]
        public async Task<IActionResult> ObtenerNoVistos()
        {
            int UsuarioId = _service.ResolverUsuarioId();
            var (response, item) = await _service.EjecutarSPPorIdLista<SeriesResumenListResponse<SeriesNoSiguiendoResponse>, SeriesNoSiguiendoResponse>("scObtenerSerieNoSiguiendo", "@UsuarioId", UsuarioId);

            if (!response.Success)
            { return BadRequest(response); }


            return Ok(item);
        }

        [HttpGet("{SerieSiguiendoId}")]
        public async Task<IActionResult> ObtenerPorId(int SerieSiguiendoId)
        {
            var (response, item) = await _service.EjecutarSPPorId<SeriesResumenListResponse<SerieSiguiendoRequest>, SerieSiguiendoRequest>("scBuscarSerieSiguiendoId", "@SerieSiguiendoId", SerieSiguiendoId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }
    }
}
