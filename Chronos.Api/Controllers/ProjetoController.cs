using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{

    public class ProjetoController : BaseController<Projeto, ProjetoRequest, ProjetoResponse>
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService) : base(projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpPost]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(201)]
        public override async Task<IActionResult> CadastrarAsync([FromBody] ProjetoRequest request)
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

        [HttpGet("{id}")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(200)]
        public override async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
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
        public override async Task<IActionResult> AlterarAsync(
            [FromRoute] int id,
            [FromBody] ProjetoRequest request
        )
        {
            var response = await _projetoService.AlterarAsync(id, request);
            return Ok(response);
        }
    }
}
