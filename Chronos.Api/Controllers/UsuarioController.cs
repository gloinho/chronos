using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    public class UsuarioController : BaseController<Usuario, UsuarioRequest, UsuarioResponse>
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) : base(usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201)]
        public override async Task<IActionResult> CadastrarAsync(UsuarioRequest request)
        {
            var response = await _usuarioService.CadastrarAsync(request);
            return Created(nameof(CadastrarAsync), response);
        }

        [AllowAnonymous]
        [HttpPost("senha")]
        public async Task<IActionResult> EnviarCodigoResetSenha([FromBody] ResetSenhaRequest request)
        {
            var response = await _usuarioService.EnviarCodigoResetSenha(request);
            return Created(nameof(CadastrarAsync), response);
        }

        [HttpPut("senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] NovaSenhaRequest request)
        {
            var response = await _usuarioService.AlterarSenha(request);
            return Created(nameof(CadastrarAsync), response);
        }
    }
}
