using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Controllers
{
    public class TarefaController : BaseController<Tarefa, TarefaRequest, TarefaResponse>
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService) : base(tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPatch("{id}/start")]
        public async Task<IActionResult> StartTarefa([FromRoute] int id)
        {
            var response = await _tarefaService.StartTarefa(id);
            return Ok(response);
        }

        [HttpPatch("{id}/stop")]
        public async Task<IActionResult> StopTarefa([FromRoute] int id)
        {
            var response = await _tarefaService.StopTarefa(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public override async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var response = await _tarefaService.DeletarAsync(id);
            return Ok(response);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObterPorUsuarioIdAsync([FromRoute] int usuarioId)
        {
            var response = await _tarefaService.ObterPorUsuarioId(usuarioId);
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

        [HttpGet("{projetoId}/projeto")]
        public async Task<IActionResult> ObterTarefasDoProjeto(int projetoId)
        {
            var response = await _tarefaService.ObterTarefasDoProjeto(projetoId);
            return Ok(response);
        }
    }
}