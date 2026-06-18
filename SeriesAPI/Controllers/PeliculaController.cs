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
    public class PeliculaController : Controller
    {
        private readonly DynamicService _service;
        GenericResponse response = new GenericResponse();

        public PeliculaController(DynamicService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] PeliculaRequest request)
        {
            response = await _service.EjecutarSPConXml("scGuardarPelicula", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Pelicula", request.Nombre, "Pelicula agregada", registroId);

            return Ok(response);
        }

        [HttpPut("{peliculaId}")]
        public async Task<IActionResult> Actualizar(int peliculaId, [FromBody] PeliculaRequest request)
        {
            request.PeliculaId = peliculaId;
            response = await _service.EjecutarSPConXml("scActualizarPelicula", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int registroId = int.TryParse(response.Result?.InnerText, out var id) ? id : 0;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Pelicula", request.Nombre, "Pelicula actualizada", registroId);

            return Ok(response);
        }

        [HttpDelete("{peliculaId}")]
        public async Task<IActionResult> Eliminar(int peliculaId)
        {
            PeliculaRequest request = new PeliculaRequest { PeliculaId = peliculaId };
            response = await _service.EjecutarSPConXml("scEliminarPelicula", request);

            if (!response.Success)
            {
                response.Message = await _service.RegistrarBitacoraError(response.Code);
                return BadRequest(response);
            }

            int id = int.TryParse(response.Result?["Id"]?.InnerText, out var parsedId) ? parsedId : 0;
            string nombre = response.Result?["Nombre"]?.InnerText!;
            response.Result = null;

            // Registrar bitácora
            await _service.RegistrarBitacoraCarga("Pelicula", nombre, "Pelicula eliminada", id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            var (response, lista) = await _service.EjecutarSPConXmlLista<object, PeliculasListResponse<PeliculasResponse>, PeliculasResponse>("scObtenerPelicula");

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(lista);
        }

        [HttpGet("{peliculaId}")]
        public async Task<IActionResult> ObtenerPorId(int peliculaId)
        {
            var (response, item) = await _service.EjecutarSPPorId<PeliculasListResponse<PeliculaResponse>, PeliculaResponse>("scBuscarPeliculaId", "@PeliculaId", peliculaId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }
    }
}
