namespace Chronos.Domain.Interfaces.Repository
{
    public interface IProjetoRepository : IBaseRepository<Projeto>
    {
        Task<ICollection<Tarefa>> GetTarefas();
    }
}
