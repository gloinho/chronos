using AutoMapper;
using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ProducesResponseType(typeof(MensagemResponse), 200)]
    [ProducesResponseType(typeof(MensagemResponse), 201)]
    [ProducesResponseType(typeof(MensagemResponse), 400)]
    [ProducesResponseType(typeof(MensagemResponse), 401)]
    [ProducesResponseType(typeof(MensagemResponse), 403)]
    [ProducesResponseType(typeof(MensagemResponse), 404)]
    [ProducesResponseType(typeof(MensagemResponse), 500)]
    [ApiController]
    public class BaseController<TEntity, KRequest, YResponse> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IBaseService<KRequest, YResponse> _service;

        public BaseController(IBaseService<KRequest, YResponse> service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        public virtual async Task<IActionResult> CadastrarAsync([FromBody] KRequest request)
        {
            var response = await _service.CadastrarAsync(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public virtual async Task<IActionResult> AlterarAsync([FromRoute] int id, [FromBody] KRequest request)
        {
            var response = await _service.AlterarAsync(id, request);
            return Ok(response);
        }

        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public virtual async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var response = await _service.DeletarAsync(id);
            return Ok(response);
        }

        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [HttpGet()]
        [ProducesResponseType(200)]
        public virtual async Task<IActionResult> ObterTodosAsync()
        {
            var response = await _service.ObterTodosAsync();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public virtual async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
        {
            var response = await _service.ObterPorIdAsync(id);
            return Ok(response);
        }
    }
}
