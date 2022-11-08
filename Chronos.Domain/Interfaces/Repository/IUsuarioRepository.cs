using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<ICollection<Tarefa>> GetTarefasPorProjeto(int projetoId);
        Task<ICollection<Projeto>> GetProjetos(int usuarioId);
    }
}
