using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Services;
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

        /// <summary>
        /// Através dessa rota você será capaz de listar as tarefas de um usuário.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna uma lista de tarefas."  </response>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObterPorUsuarioIdAsync([FromRoute] int usuarioId)
        {
            var response = await _tarefaService.ObterPorUsuarioId(usuarioId);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de listar as tarefas de um usuário a partir de um filtro.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="filter_by"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna uma lista de tarefas." </response>
        [HttpGet("usuario/{usuarioId}/filter_by")]
        public async Task<IActionResult> ObterTarefasDoUsuarioPorFiltro(
            [FromRoute] int usuarioId,
            [FromQuery] FiltroRequest filter_by
        )
        {
            var response = await _tarefaService.ObterTarefasPorFiltro(usuarioId, filter_by);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de listar as tarefas de um projeto.
        /// </summary>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna uma lista de tarefas." </response>
        [HttpGet("projeto/{projetoId}")]
        public async Task<IActionResult> ObterTarefasDoProjeto(int projetoId)
        {
            var response = await _tarefaService.ObterTarefasDoProjeto(projetoId);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de iniciar uma tarefa.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Retorna a tarefa que foi iniciada.</response>
        [HttpPatch("{id}/start")]
        public async Task<IActionResult> StartTarefa([FromRoute] int id)
        {
            var response = await _tarefaService.StartTarefa(id);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de finalizar uma tarefa.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Retorna a tarefa que foi finalizada.</response>
        [HttpPatch("{id}/stop")]
        public async Task<IActionResult> StopTarefa([FromRoute] int id)
        {
            var response = await _tarefaService.StopTarefa(id);
            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de deletar uma tarefa.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna mensagem de sucesso.  </response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public override async Task<IActionResult> DeletarAsync([FromRoute] int id)
        {
            var response = await _tarefaService.DeletarAsync(id);
            return Ok(response);
        }


    }
}
