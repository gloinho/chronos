using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] TarefaRequest request)
        {
            var response = await _tarefaService.CadastrarAsync(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAsync([FromRoute] int id)
        {
            var response = await _tarefaService.DeletarAsync(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarAsync(
            [FromRoute] int id,
            [FromBody] TarefaRequest request
        )
        {
            var response = await _tarefaService.AlterarAsync(id, request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
        {
            var response = await _tarefaService.ObterPorIdAsync(id);
            return Ok(response);
        }

        [Authorize(Roles = PermissaoUtil.PermissaoAdministrador)]
        [HttpGet]
        public async Task<IActionResult> ObterTodosAsync()
        {
            var response = await _tarefaService.ObterTodosAsync();
            return Ok(response);
        }

        [HttpGet("{usuarioId}/dia")]
        public async Task<IActionResult> ObterTarefasDoDia([FromRoute] int usuarioId)
        {
            var response = await _tarefaService.ObterTarefasDoDia(usuarioId);
            return Ok(response);
        }

        [HttpGet("{usuarioId}/mes")]
        public async Task<IActionResult> ObterTarefasDoMes([FromRoute] int usuarioId)
        {
            var response = await _tarefaService.ObterTarefasDoMes(usuarioId);
            return Ok(response);
        }

        [HttpGet("{usuarioId}/semana")]
        public async Task<IActionResult> ObterTarefasDaSemana([FromRoute] int usuarioId)
        {
            var response = await _tarefaService.ObterTarefasDaSemana(usuarioId);
            return Ok(response);
        }
    }
}
