using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        Task<MensagemResponse> Login(LoginRequest request);
        Task<MensagemResponse> Confirmar(string token);
    }
}
