using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Enums;
using Modelos.Request;
using Modelos.Response;
using Negocio;

namespace SeriesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TemporadaCaricaturaController : Controller
    {
        private readonly DynamicService _service;
        GenericResponse response = new GenericResponse();

        public TemporadaCaricaturaController(DynamicService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] TemporadaCaricaturaRequest request)
        {
            response = await _service.EjecutarSPConXml("scGuardarTemporadaCaricatura", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?["Id"]?.InnerText, out var parsedId) ? parsedId : 0;
            string nombre = response.Result?["Nombre"]?.InnerText!;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga(SeccionBitacora.TemporadaCaricatura, nombre, ProcesoBitacora.Agregado, registroId);

            return Ok(response);
        }

        [HttpPut("{temporadaId}")]
        public async Task<IActionResult> Actualizar(int temporadaId, [FromBody] TemporadaCaricaturaRequest request)
        {
            request.TemporadaId = temporadaId;
            response = await _service.EjecutarSPConXml("scActualizarTemporadaCaricatura", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?["Id"]?.InnerText, out var parsedId) ? parsedId : 0;
            string nombre = response.Result?["Nombre"]?.InnerText!;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga(SeccionBitacora.TemporadaCaricatura, nombre, ProcesoBitacora.Actualizado, registroId);

            return Ok(response);
        }

        [HttpDelete("{temporadaId}")]
        public async Task<IActionResult> Eliminar(int temporadaId)
        {
            TemporadaCaricaturaRequest request = new TemporadaCaricaturaRequest { TemporadaId = temporadaId };
            response = await _service.EjecutarSPConXml("scEliminarTemporadaCaricatura", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?["Id"]?.InnerText, out var parsedId) ? parsedId : 0;
            string nombre = response.Result?["Nombre"]?.InnerText!;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga(SeccionBitacora.TemporadaCaricatura, nombre, ProcesoBitacora.Eliminado, registroId);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            var (response, lista) = await _service.EjecutarSPConXmlLista<object, TemporadasCaricaturaListResponse<TemporadasCaricaturaResponse>, TemporadasCaricaturaResponse>("scObtenerTemporadaCaricatura");

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(lista);
        }

        [HttpGet("{temporadaId}")]
        public async Task<IActionResult> ObtenerPorId(int temporadaId)
        {
            var (response, item) = await _service.EjecutarSPPorId<TemporadasCaricaturaListResponse<TemporadaCaricaturaResponse>, TemporadaCaricaturaResponse>("scBuscarTemporadaCaricaturaId", "@TemporadaId", temporadaId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }
    }
}
