using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CadastrarAsync([FromBody] ProjetoRequest request)
        {
            var response = await _projetoService.CadastrarAsync(request);
            return Created(nameof(CadastrarAsync), response);
        }

        [HttpPost("{id}/colaboradores")]
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
        [ProducesResponseType(200)]
        public async Task<IActionResult> ObterTodosAsync()
        {
            var response = await _projetoService.ObterTodosAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
        {
            var response = await _projetoService.ObterPorIdAsync(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
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
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeletarAsync([FromRoute] int id)
        {
            var response = await _projetoService.DeletarAsync(id);
            return Ok(response);
        }
    }
}
