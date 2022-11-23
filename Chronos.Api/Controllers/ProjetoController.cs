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


        /// <summary>
        /// Através dessa rota você será capaz de cadastrar um registro no banco
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso, e retorna a mensagem  "Cadastrado com Sucesso" </response>
        [HttpPost]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(201)]
        public override async Task<IActionResult> CadastrarAsync([FromBody] ProjetoRequest request)
        {
            var response = await _projetoService.CadastrarAsync(request);
            return Created(nameof(CadastrarAsync), response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de adicionar um colaborador há um projeto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso, e retorna a mensagem  "Cadastrado com Sucesso" </response>
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

        /// <summary>
        /// Através dessa rota você será capaz de buscar um projeto pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna um projeto</response>
        [HttpGet("{id}")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(200)]
        public override async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
        {
            var response = await _projetoService.ObterPorIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de buscar um projeto pelo UsuarioId
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna um projeto</response>
        [Authorize(Roles = PermissaoUtil.PermissaoDupla)]
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObterPorUsuarioIdAsync([FromRoute] int usuarioId)
        {
            var response = await _projetoService.ObterPorUsuarioId(usuarioId);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de alterar um projeto do banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna a mensagem "Alterado com Sucesso" </response>
        [HttpPut("{id}")]
        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [ProducesResponseType(200)]
        public override async Task<IActionResult> AlterarAsync([FromRoute] int id, [FromBody] ProjetoRequest request)
        {
            var response = await _projetoService.AlterarAsync(id, request);
            return Ok(response);
        }
    }
}
