using System.Linq.Expressions;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IBaseService<TRequest, TResponse>
    {
        Task<List<TResponse>> ObterTodosAsync();
        Task<TResponse> ObterPorIdAsync(int id);
        Task CadastrarAsync(TRequest request);
        Task DeletarAsync(int id);
        Task AlterarAsync(int id, TRequest request);
    }
}
