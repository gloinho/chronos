using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(MensagemResponse), 200)]
    [ProducesResponseType(typeof(MensagemResponse), 201)]
    [ProducesResponseType(typeof(MensagemResponse), 400)]
    [ProducesResponseType(typeof(MensagemResponse), 401)]
    [ProducesResponseType(typeof(MensagemResponse), 403)]
    [ProducesResponseType(typeof(MensagemResponse), 404)]
    [ProducesResponseType(typeof(MensagemResponse), 500)]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpPost]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CadastrarAsync([FromBody] ProjetoRequest request)
        {
            var response = await _projetoService.CadastrarAsync(request);
            return Created(nameof(CadastrarAsync), response);
        }

        [HttpPost("{id}/colaboradores")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> AdicionarColaboradoresAsync(
            [FromRoute] int id,
            [FromBody] AdicionarColaboradoresRequest request
        )
        {
            var response = await _projetoService.AdicionarColaboradores(id, request);
            return Created(nameof(AdicionarColaboradoresAsync), response);
        }

        [HttpGet]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ObterTodosAsync()
        {
            var response = await _projetoService.ObterTodosAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
        {
            var response = await _projetoService.ObterPorIdAsync(id);
            return Ok(response);
        }

        [Authorize(Roles = PermissaoUtil.PermissaoDupla)]
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObterPorUsuarioIdAsync([FromRoute] int usuarioId)
        {
            var response = await _projetoService.ObterPorUsuarioId(usuarioId);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AlterarAsync(
            [FromRoute] int id,
            [FromBody] ProjetoRequest request
        )
        {
            var response = await _projetoService.AlterarAsync(id, request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeletarAsync([FromRoute] int id)
        {
            var response = await _projetoService.DeletarAsync(id);
            return Ok(response);
        }
    }
}
