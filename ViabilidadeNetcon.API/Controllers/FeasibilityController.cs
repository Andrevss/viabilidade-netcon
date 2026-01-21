using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViabilidadeNetcon.Application.DTOs;
using ViabilidadeNetcon.Application.Services;
using ViabilidadeNetcon.Infrastructure.Repositories;

namespace ViabilidadeNetcon.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class FeasibilityController : ControllerBase
    {
        private readonly IViabilidadeService _service;

        public FeasibilityController(IViabilidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AtivoResponseDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponseDto), 400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetFeasibility(
            [FromQuery] double? latitude,
            [FromQuery] double? longitude,
            [FromQuery] int? radius)
        {
            if (!latitude.HasValue)
            {
                return BadRequest(new ErrorResponseDto(
                    "400",
                    "empty field",
                    "latitude is mandatory",
                    "bad request"
                ));
            }

            if (!longitude.HasValue)
            {
                return BadRequest(new ErrorResponseDto(
                    "400",
                    "empty field",
                    "longitude is mandatory",
                    "bad request"
                ));
            }

            if (!radius.HasValue)
            {
                return BadRequest(new ErrorResponseDto(
                    "400",
                    "empty field",
                    "radius is mandatory",
                    "bad request"
                ));
            }

            try
            {
                var ativos = _service.BuscarAtivosNoRaio(
                    latitude.Value,
                    longitude.Value,
                    radius.Value
                );

                Response.Headers.Add("X-Request-Id", Guid.NewGuid().ToString());
                Response.Headers.Add("Cache-Control", "no-store");

                return Ok(ativos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponseDto(
                    "400",
                    "validation error",
                    ex.Message,
                    "bad request"
                ));
            }
        }
    }
}