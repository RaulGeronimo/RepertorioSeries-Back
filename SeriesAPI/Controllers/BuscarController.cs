using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Response;
using Negocio;

namespace SeriesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BuscarController : Controller
    {
        private readonly DynamicService _service;

        public BuscarController(DynamicService service)
        {
            _service = service;
        }

        #region Caricaturas
        [HttpGet("Caricatura/{caricaturaId}")]
        public async Task<IActionResult> BuscarCaricatura(int caricaturaId)
        {
            var (response, item) = await _service.EjecutarSPPorId<CaricaturasListResponse<CaricaturasResponse>, CaricaturasResponse>("scObtenerCaricaturaId", "@CaricaturaId", caricaturaId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }

        [HttpGet("Caricatura/Temporadas/{caricaturaId}")]
        public async Task<IActionResult> ObtenerTemporadasCaricatura(int caricaturaId)
        {
            var (response, item) = await _service.EjecutarSPPorIdLista<TemporadasCaricaturaListResponse<TemporadasCaricaturaResponse>, TemporadasCaricaturaResponse>("scBuscarTemporadasCaricaturaId", "@CaricaturaId", caricaturaId);

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(item);
        }

        [HttpGet("Caricatura/Peliculas/{caricaturaId}")]
        public async Task<IActionResult> ObtenerPeliculasCaricatura(int caricaturaId)
        {
            var (response, item) = await _service.EjecutarSPPorIdLista<PeliculasListResponse<PeliculasResponse>, PeliculasResponse>("scBuscarPeliculasCaricaturaId", "@CaricaturaId", caricaturaId);

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(item);
        }
        #endregion Caricaturas

        #region Series
        [HttpGet("Serie/{serieId}")]
        public async Task<IActionResult> BuscarSerie(int serieId)
        {
            var (response, item) = await _service.EjecutarSPPorId<SeriesListResponse<SeriesResponse>, SeriesResponse>("scObtenerSerieId", "@SerieId", serieId);

            if (!response.Success)
            { return BadRequest(response); }

            if (item == null)
            { return NotFound(response); }

            return Ok(item);
        }

        [HttpGet("Serie/Temporadas/{serieId}")]
        public async Task<IActionResult> ObtenerTemporadasSerie(int serieId)
        {
            var (response, item) = await _service.EjecutarSPPorIdLista<TemporadasSerieListResponse<TemporadasSerieResponse>, TemporadasSerieResponse>("scBuscarTemporadasSerieId", "@SerieId", serieId);

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(item);
        }

        [HttpGet("Serie/Peliculas/{serieId}")]
        public async Task<IActionResult> ObtenerPeliculasSerie(int serieId)
        {
            var (response, item) = await _service.EjecutarSPPorIdLista<PeliculasListResponse<PeliculasResponse>, PeliculasResponse>("scBuscarPeliculasSerieId", "@SerieId", serieId);

            if (!response.Success)
            { return BadRequest(response); }

            return Ok(item);
        }
        #endregion Series
    }
}
