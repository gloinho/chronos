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

        /// <summary>
        /// Atrav�s dessa rota voc� ser� capaz de cadastrar um Usuario no banco de dados
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Conclua a configura��o da sua nova Conta Chronos com o token: </response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201)]
        public override async Task<IActionResult> CadastrarAsync(UsuarioRequest request)
        {
            var response = await _usuarioService.CadastrarAsync(request);
            return Created(nameof(CadastrarAsync), response);
        }

        /// <summary>
        /// Atr�ves dessa rota ser� enviado um c�digo para resetar sua senha.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Mensagem: "Enviamos o c�digo de altera��o de senha para seu e-mail. Este c�digo 
        /// ser� v�lido por apenas 2 horas." </response>
        [AllowAnonymous]
        [HttpPost("senha")]
        public async Task<IActionResult> EnviarCodigoResetSenha([FromBody] ResetSenhaRequest request)
        {
            var response = await _usuarioService.EnviarCodigoResetSenha(request);
            return Created(nameof(CadastrarAsync), response);
        }

        /// <summary>
        /// Atrav�s dessa rota voc� ser� capaz de alterar sua senha.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Mensagem: "Senha alterada com sucesso."  </response>
        [HttpPut("senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] NovaSenhaRequest request)
        {
            var response = await _usuarioService.AlterarSenha(request);
            return Created(nameof(CadastrarAsync), response);
        }
    }
}
