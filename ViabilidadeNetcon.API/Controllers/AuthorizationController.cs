using Microsoft.AspNetCore.Mvc;
using ViabilidadeNetcon.Application.DTOs;
using ViabilidadeNetcon.Infrastructure.Security;

namespace ViabilidadeNetcon.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthorizationController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponseDto), 401)]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (!_jwtService.ValidarCredenciais(request.Name, request.Password))
            {
                return Unauthorized(new ErrorResponseDto(
                    "401",
                    "invalid credentials",
                    "Invalid username or password",
                    "unauthorized"
                ));
            }

            var token = _jwtService.GerarToken(request.Name);

            return Ok(new LoginResponseDto { Token = token });
        }
    }
}
