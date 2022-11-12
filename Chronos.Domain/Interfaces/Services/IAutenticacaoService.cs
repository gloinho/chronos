using Chronos.Domain.Contracts.Request;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        Task<string> Login(LoginRequest request);
        Task Confirmar(string token);
    }
}
