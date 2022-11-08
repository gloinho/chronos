namespace Chronos.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Cadastrar(T entidade);
        Task<T> Editar(T entidade);
        Task<ICollection<T>> Listar();
        Task<T> GetPorId(int id);
        Task<T> Excluir(T entidade);
    }
}
