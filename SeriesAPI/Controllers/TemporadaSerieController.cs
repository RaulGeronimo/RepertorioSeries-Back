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
    public class TemporadaSerieController : Controller
    {
        private readonly DynamicService _service;
        GenericResponse response = new GenericResponse();

        public TemporadaSerieController(DynamicService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] TemporadaSerieRequest request)
        {
            response = await _service.EjecutarSPConXml("scGuardarTemporadaSerie", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Temporada", request.Nombre, "Temporada agregada", registroId);

            return Ok(response);
        }

        [HttpPut("{temporadaId}")]
        public async Task<IActionResult> Actualizar(int temporadaId, [FromBody] TemporadaSerieRequest request)
        {
            request.TemporadaId = temporadaId;
            response = await _service.EjecutarSPConXml("scActualizarTemporadaSerie", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Temporada", request.Nombre, "Temporada actualizada", registroId);

            return Ok(response);
        }

        [HttpDelete("{temporadaId}")]
        public async Task<IActionResult> Eliminar(int temporadaId)
        {
            TemporadaSerieRequest request = new TemporadaSerieRequest { TemporadaId = temporadaId };
            response = await _service.EjecutarSPConXml("scEliminarTemporadaSerie", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int id = int.TryParse(response.Result?["Id"]?.InnerText, out var parsedId) ? parsedId : 0;
            string nombre = response.Result?["Nombre"]?.InnerText!;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Temporada", nombre, "Temporada eliminada", id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            var (response, lista) = await _service.EjecutarSPConXmlLista<object, TemporadasSerieListResponse<TemporadasSerieResponse>, TemporadasSerieResponse>("scObtenerTemporadaSerie");

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(lista);
        }

        [HttpGet("{temporadaId}")]
        public async Task<IActionResult> ObtenerPorId(int temporadaId)
        {
            var (response, item) = await _service.EjecutarSPPorId<TemporadasSerieListResponse<TemporadaSerieResponse>, TemporadaSerieResponse>("scBuscarTemporadaSerieId", "@TemporadaId", temporadaId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }
    }
}
