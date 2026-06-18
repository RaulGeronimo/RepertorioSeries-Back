using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace SeriesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogosController : Controller
    {
        private readonly DynamicService _service;
        public CatalogosController(DynamicService service)
        {
            _service = service;
        }

        [HttpGet("Rol")]
        public async Task<IActionResult> ObtenerRol()
        {
            var (response, resultados) = await _service.ObtenerCatalogoDinamico("Rol");

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(resultados);
        }

        [HttpGet("Clasificacion")]
        public async Task<IActionResult> ObtenerClasificacion()
        {
            var (response, resultados) = await _service.ObtenerCatalogoDinamico("Clasificacion");

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(resultados);
        }
    }
}
