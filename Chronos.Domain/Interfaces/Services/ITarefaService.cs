using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;

namespace Chronos.Domain.Interfaces.Services
{
    public interface ITarefaService : IBaseService<TarefaRequest, TarefaResponse>
    {
        Task<List<TarefaResponse>> ObterPorUsuarioId(int usuarioId);
        Task<List<TarefaResponse>> ObterTarefasPorFiltro(int usuarioId, FiltroRequest request);
        Task<List<TarefaResponse>> ObterTarefasDoProjeto(int projetoId);
        Task<TarefaResponse> StartTarefa(int id);
        Task<TarefaResponse> StopTarefa(int id);
    }
}
