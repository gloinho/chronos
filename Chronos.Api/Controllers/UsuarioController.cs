using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Chronos.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CadastrarAsync(UsuarioRequest request)
        {
            var response = await _usuarioService.CadastrarAsync(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarAsync(
            [FromRoute] int id,
            [FromBody] UsuarioRequest request
        )
        {
            var response = await _usuarioService.AlterarAsync(id, request);
            return Ok(response);
        }
    }
}
