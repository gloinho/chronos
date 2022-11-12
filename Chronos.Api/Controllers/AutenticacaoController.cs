using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarUsuario([FromQuery] string token)
        {
            try
            {
                await _autenticacaoService.Confirmar(token);
                return Ok("Confirmação feita com sucesso.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _autenticacaoService.Login(request);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
