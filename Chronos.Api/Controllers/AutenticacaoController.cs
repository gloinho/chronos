using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(MensagemResponse), 200)]
    [ProducesResponseType(typeof(MensagemResponse), 201)]
    [ProducesResponseType(typeof(MensagemResponse), 400)]
    [ProducesResponseType(typeof(MensagemResponse), 401)]
    [ProducesResponseType(typeof(MensagemResponse), 403)]
    [ProducesResponseType(typeof(MensagemResponse), 404)]
    [ProducesResponseType(typeof(MensagemResponse), 500)]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        /// <summary>
        /// Atrav�s dessa rota voc� ser� capaz de confirmar o e-mail de um usu�rio.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna mensagem de sucesso. </response>
        [HttpPost]
        public async Task<IActionResult> ConfirmarUsuario([FromQuery] string token)
        {
            var response = await _autenticacaoService.Confirmar(token);
            return Ok(response);
        }

        /// <summary>
        /// Atrav�s dessa rota voc� ser� capaz de realizar o login do usu�rio para ter acesso aos endpoints da aplica��o.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna mensagem de sucesso com o Token para autentica��o. </response>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _autenticacaoService.Login(request);
            return Ok(response);
        }
    }
}
