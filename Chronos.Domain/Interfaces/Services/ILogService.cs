

using Chronos.Domain.Contracts.Response;

namespace Chronos.Domain.Interfaces.Services
{
    public interface ILogService
    {
        Task<MensagemResponse> LogAsync(string localAcao, string acao, int alterado);
        Task<MensagemResponse> LogAlterarSenha(string localAcao, string acao, int alterado, int responsavel);
    }
}
