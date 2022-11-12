using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Repository
{
    public interface ITarefaRepository : IBaseRepository<Tarefa> 
    {
        Task<Tarefa> GetTarefasMes(int usuarioId);
        Task<Tarefa> GetTarefasDia(int usuarioId);
        Task<Tarefa> GetTarefasSemana(int usuarioId);
        Task<Tarefa> GetTarefasProjeto(int projetoId);
     }
}
