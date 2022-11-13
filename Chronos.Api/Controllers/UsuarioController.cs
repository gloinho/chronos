using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Chronos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAsync(UsuarioRequest request)
        {
            await _usuarioService.CadastrarAsync(request);
            return Ok(
                new Response
                {
                    Mensagem =
                        "Enviamos um token para seu email. Por favor, faça a confirmação."
                }
            );
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            return Ok(usuario);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarAsync(
            [FromRoute] int id,
            [FromBody] UsuarioRequest request
        )
        {
            await _usuarioService.AlterarAsync(id, request);
            return Ok(new Response { Mensagem = "Usuario Editado." });
        }
    }
}
