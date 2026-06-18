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
    public class CaricaturaController : Controller
    {
        private readonly DynamicService _service;
        GenericResponse response = new GenericResponse();

        public CaricaturaController(DynamicService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CaricaturaRequest request)
        {
            response = await _service.EjecutarSPConXml("scGuardarCaricatura", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Caricatura", request.Nombre, "Caricatura agregada", registroId);

            return Ok(response);
        }

        [HttpPut("{CaricaturaId}")]
        public async Task<IActionResult> Actualizar(int CaricaturaId, [FromBody] CaricaturaRequest request)
        {
            request.CaricaturaId = CaricaturaId;
            response = await _service.EjecutarSPConXml("scActualizarCaricatura", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Caricatura", request.Nombre, "Caricatura actualizada", registroId);

            return Ok(response);
        }

        [HttpDelete("{CaricaturaId}")]
        public async Task<IActionResult> Eliminar(int CaricaturaId)
        {
            CaricaturaRequest request = new CaricaturaRequest { CaricaturaId = CaricaturaId };
            response = await _service.EjecutarSPConXml("scEliminarCaricatura", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int id = int.TryParse(response.Result?["Id"]?.InnerText, out var parsedId) ? parsedId : 0;
            string nombre = response.Result?["Nombre"]?.InnerText!;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Caricatura", nombre, "Caricatura eliminada", id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            var (response, lista) = await _service.EjecutarSPConXmlLista<object, CaricaturasListResponse<CaricaturasResponse>, CaricaturasResponse>("scObtenerCaricatura");

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(lista);
        }

        [HttpGet("{CaricaturaId}")]
        public async Task<IActionResult> ObtenerPorId(int CaricaturaId)
        {
            var (response, item) = await _service.EjecutarSPPorId<CaricaturasListResponse<CaricaturaResponse>, CaricaturaResponse>("scBuscarCaricaturaId", "@CaricaturaId", CaricaturaId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }
    }
}
