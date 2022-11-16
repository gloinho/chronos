using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<UsuarioRequest, UsuarioResponse>
    {
        Task<MensagemResponse> AlterarSenha(RecuperarSenhaRequest request);
        Task<MensagemResponse> EnviarCondigoRecuperarSenha(CodigoRecuperarSenhaRequest request);
    }
}
