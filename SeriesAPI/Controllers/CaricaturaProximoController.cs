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
    public class CaricaturaProximoController : Controller
    {
        private readonly DynamicService _service;
        GenericResponse response = new GenericResponse();

        public CaricaturaProximoController(DynamicService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CaricaturaProximoRequest request)
        {
            request.UsuarioId = _service.ResolverUsuarioId();
            response = await _service.EjecutarSPConXml("scGuardarCaricaturaProximo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            return Ok(response);
        }

        [HttpPut("{caricaturaProximoId}")]
        public async Task<IActionResult> Actualizar(int caricaturaProximoId, [FromBody] CaricaturaProximoRequest request)
        {
            request.UsuarioId = _service.ResolverUsuarioId();
            request.CaricaturaProximoId = caricaturaProximoId;
            response = await _service.EjecutarSPConXml("scActualizarCaricaturaProximo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            return Ok(response);
        }

        [HttpDelete("{caricaturaProximoId}")]
        public async Task<IActionResult> Eliminar(int caricaturaProximoId)
        {
            CaricaturaProximoRequest request = new CaricaturaProximoRequest { CaricaturaProximoId = caricaturaProximoId };
            response = await _service.EjecutarSPConXml("scEliminarCaricaturaProximo", request);

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
            var (response, item) = await _service.EjecutarSPPorIdLista<CaricaturasResumenListResponse<CaricaturasProximoResponse>, CaricaturasProximoResponse>("scObtenerCaricaturaProximo", "@UsuarioId", UsuarioId);

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(item);
        }

        [HttpGet("{caricaturaProximoId}")]
        public async Task<IActionResult> ObtenerPorId(int caricaturaProximoId)
        {
            var (response, item) = await _service.EjecutarSPPorId<CaricaturasResumenListResponse<CaricaturaProximoResponse>, CaricaturaProximoResponse>("scBuscarCaricaturaProximoId", "@CaricaturaProximoId", caricaturaProximoId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }
    }
}
