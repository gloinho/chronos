using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;

namespace Chronos.Domain.Interfaces.Services
{
    public interface ITarefaService : IBaseService<TarefaRequest, TarefaResponse>
    {
        Task<List<TarefaResponse>> ObterTarefasDoDia(int usuarioId);
        Task<List<TarefaResponse>> ObterTarefasDoMes(int usuarioId);
        Task<List<TarefaResponse>> ObterTarefasDaSemana(int usuarioId);
    }
}
