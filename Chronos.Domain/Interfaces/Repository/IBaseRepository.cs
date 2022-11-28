using System.Linq.Expressions;

namespace Chronos.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task CadastrarAsync(T entidade);
        Task<T> AlterarAsync(T entidade);
        Task<ICollection<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task DeletarAsync(T entidade);
        Task<T> ObterAsync(Expression<Func<T, bool>> expression);
    }
}
