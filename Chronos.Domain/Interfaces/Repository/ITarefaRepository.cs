using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Repository
{
    public interface ITarefaRepository : IBaseRepository<Tarefa>
    {
        Task<List<Tarefa>> GetTarefasMes(int usuarioId);
        Task<List<Tarefa>> GetTarefasDia(int usuarioId);
        Task<List<Tarefa>> GetTarefasSemana(int usuarioId);
        Task<List<Tarefa>> GetTarefasProjeto(int projetoId);
        Task<List<Tarefa>> ObterPorUsuarioIdAsync(int usuarioId);
    }
}
