using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<Response> CadastrarUsuario(UsuarioRequest request);
    }
}
