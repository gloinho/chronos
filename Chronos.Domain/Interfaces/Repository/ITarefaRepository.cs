using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Repository
{
    public interface ITarefaRepository : IBaseRepository<Tarefa>
    {
        Task<List<Usuario_Projeto>> GetTarefasMes(int usuarioId);
        Task<List<Usuario_Projeto>> GetTarefasDia(int usuarioId);
        Task<List<Usuario_Projeto>> GetTarefasSemana(int usuarioId);
        Task<List<Usuario_Projeto>> GetTarefasProjeto(int projetoId);
    }
}
