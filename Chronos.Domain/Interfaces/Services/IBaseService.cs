using Chronos.Domain.Contracts.Response;
using System.Linq.Expressions;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IBaseService<TRequest, TResponse>
    {
        Task<List<TResponse>> ObterTodosAsync();
        Task<TResponse> ObterPorIdAsync(int id);
        Task<MensagemResponse> CadastrarAsync(TRequest request);
        Task<MensagemResponse> DeletarAsync(int id);
        Task<MensagemResponse> AlterarAsync(int id, TRequest request);
    }
}
