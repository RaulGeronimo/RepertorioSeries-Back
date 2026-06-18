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
    public class SerieProximoController : Controller
    {
        private readonly DynamicService _service;
        GenericResponse response = new GenericResponse();

        public SerieProximoController(DynamicService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] SerieProximoRequest request)
        {
            request.UsuarioId = _service.ResolverUsuarioId();
            response = await _service.EjecutarSPConXml("scGuardarSerieProximo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            return Ok(response);
        }

        [HttpPut("{SerieProximoId}")]
        public async Task<IActionResult> Actualizar(int SerieProximoId, [FromBody] SerieProximoRequest request)
        {
            request.UsuarioId = _service.ResolverUsuarioId();
            request.SerieProximoId = SerieProximoId;
            response = await _service.EjecutarSPConXml("scActualizarSerieProximo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            return Ok(response);
        }

        [HttpDelete("{SerieProximoId}")]
        public async Task<IActionResult> Eliminar(int SerieProximoId)
        {
            SerieProximoRequest request = new SerieProximoRequest { SerieProximoId = SerieProximoId };
            response = await _service.EjecutarSPConXml("scEliminarSerieProximo", request);

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

        [HttpGet]
        public async Task<IActionResult> ObtenerProximos()
        {
            int UsuarioId = _service.ResolverUsuarioId();
            var (response, item) = await _service.EjecutarSPPorIdLista<SeriesResumenListResponse<SeriesProximoResponse>, SeriesProximoResponse>("scObtenerSerieProximo", "@UsuarioId", UsuarioId);

            if (!response.Success)
            { return BadRequest(response); }


            return Ok(item);
        }

        [HttpGet("{SerieProximoId}")]
        public async Task<IActionResult> ObtenerPorId(int SerieProximoId)
        {
            var (response, item) = await _service.EjecutarSPPorId<SeriesResumenListResponse<SerieProximoResponse>, SerieProximoResponse>("scBuscarSerieProximoId", "@SerieProximoId", SerieProximoId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }
    }
}
