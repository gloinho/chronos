using Chronos.Domain.Contracts.Request;
<<<<<<< HEAD
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Entities.Enums;
=======
>>>>>>> origin/main
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
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

        /// <summary>
        /// Através dessa rota você será capaz de alterar a permissão de um Usuario.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissao"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna a mensagem "Permissão alterada com sucesso"  </response>
        [HttpPatch("{id}")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        public async Task<IActionResult> MudarPermissao([FromRoute] int id, [FromBody] Permissao permissao)
        {
            var response = await _usuarioService.MudarPermissao(id, permissao);
            return Ok(response);
        }
    }
}
