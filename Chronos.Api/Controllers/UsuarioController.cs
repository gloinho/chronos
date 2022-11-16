using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ProducesResponseType(typeof(MensagemResponse), 200)]
    [ProducesResponseType(typeof(MensagemResponse), 201)]
    [ProducesResponseType(typeof(MensagemResponse), 400)]
    [ProducesResponseType(typeof(MensagemResponse), 401)]
    [ProducesResponseType(typeof(MensagemResponse), 403)]
    [ProducesResponseType(typeof(MensagemResponse), 404)]
    [ProducesResponseType(typeof(MensagemResponse), 500)]
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
            return Created(nameof(CadastrarAsync), response);
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

        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [HttpGet]
        public async Task<IActionResult> ObterTodosAsync()
        {
            var response = await _usuarioService.ObterTodosAsync();
            return Ok(response);
        }

        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [HttpDelete]
        public async Task<IActionResult> DeletarAsync(int id)
        {
            var response = await _usuarioService.DeletarAsync(id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Enviar-CodigoSenha")]
        public async Task<IActionResult> SolicitarCodigoRecuperarSenha(CodigoRecuperarSenhaRequest request)
        {
            var response = await _usuarioService.EnviarCondigoRecuperarSenha(request);
            return Created(nameof(CadastrarAsync), response);
        }

        // Quando Coloco uma autorização, esta dando falha.
        [AllowAnonymous]
        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarSenha(RecuperarSenhaRequest request)
        {
            var response = await _usuarioService.AlterarSenha(request);
            return Created(nameof(CadastrarAsync), response);
        }
    }
}
