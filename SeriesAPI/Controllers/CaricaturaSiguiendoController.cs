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
    public class CaricaturaSiguiendoController : Controller
    {
        private readonly DynamicService _service;
        GenericResponse response = new GenericResponse();

        public CaricaturaSiguiendoController(DynamicService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CaricaturaSiguiendoRequest request)
        {
            request.UsuarioId = _service.ResolverUsuarioId();
            response = await _service.EjecutarSPConXml("scGuardarCaricaturaSiguiendo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            return Ok(response);
        }

        [HttpPut("{caricaturaSiguiendoId}")]
        public async Task<IActionResult> Actualizar(int caricaturaSiguiendoId, [FromBody] CaricaturaSiguiendoRequest request)
        {
            request.UsuarioId = _service.ResolverUsuarioId();
            request.CaricaturaSiguiendoId = caricaturaSiguiendoId;
            response = await _service.EjecutarSPConXml("scActualizarCaricaturaSiguiendo", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            return Ok(response);
        }

        [HttpDelete("{caricaturaSiguiendoId}")]
        public async Task<IActionResult> Eliminar(int caricaturaSiguiendoId)
        {
            CaricaturaSiguiendoRequest request = new CaricaturaSiguiendoRequest { CaricaturaSiguiendoId = caricaturaSiguiendoId };
            response = await _service.EjecutarSPConXml("scEliminarCaricaturaSiguiendo", request);

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
            var (response, item) = await _service.EjecutarSPPorIdLista<CaricaturasResumenListResponse<CaricaturasSiguiendoResponse>, CaricaturasSiguiendoResponse>("scObtenerCaricaturaSiguiendo", "@UsuarioId", UsuarioId);

            if (!response.Success)
            { return BadRequest(response); }


            return Ok(item);
        }

        [HttpGet("NoVistos")]
        public async Task<IActionResult> ObtenerNoVistos()
        {
            int UsuarioId = _service.ResolverUsuarioId();
            var (response, item) = await _service.EjecutarSPPorIdLista<CaricaturasResumenListResponse<CaricaturasNoSiguiendoResponse>, CaricaturasNoSiguiendoResponse>("scObtenerCaricaturaNoSiguiendo", "@UsuarioId", UsuarioId);

            if (!response.Success)
            { return BadRequest(response); }


            return Ok(item);
        }

        [HttpGet("{caricaturaSiguiendoId}")]
        public async Task<IActionResult> ObtenerPorId(int caricaturaSiguiendoId)
        {
            var (response, item) = await _service.EjecutarSPPorId<CaricaturasResumenListResponse<CaricaturaSiguiendoResponse>, CaricaturaSiguiendoResponse>("scBuscarCaricaturaSiguiendoId", "@CaricaturaSiguiendoId", caricaturaSiguiendoId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }
    }
}
