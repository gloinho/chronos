using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Entities.Enums;
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

        /// <summary>
        /// Através dessa rota você será capaz de cadastrar um usuário.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Sucesso, e retorna mensagem de sucesso com instruções para utilização da aplicação.</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201)]
        public override async Task<IActionResult> CadastrarAsync(UsuarioRequest request)
        {
            var response = await _usuarioService.CadastrarAsync(request);
            return Created(nameof(CadastrarAsync), response);
        }

        /// <summary>
        /// Atráves dessa rota será enviado um código para resetar sua senha.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna mensagem de sucesso com instruções para reset de senha. </response>
        [AllowAnonymous]
        [HttpPost("senha")]
        public async Task<IActionResult> EnviarCodigoResetSenha(
            [FromBody] ResetSenhaRequest request
        )
        {
            var response = await _usuarioService.EnviarCodigoResetSenha(request);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de alterar sua senha.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna mensagem de sucesso. </response>
        [HttpPut("senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] NovaSenhaRequest request)
        {
            var response = await _usuarioService.AlterarSenha(request);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de mudar a permissão de um Usuario.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissao"></param>
        /// <returns></returns>
        /// <response code="200">   </response>
        [HttpPatch("{id}")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        public async Task<IActionResult> MudarPermissao([FromRoute] int id, [FromBody] Permissao permissao)
        {
            var response = await _usuarioService.MudarPermissao(id, permissao);
            return Ok(response);
        }
    }
}
