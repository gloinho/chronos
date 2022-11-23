using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<UsuarioRequest, UsuarioResponse>
    {
        Task<MensagemResponse> AlterarSenha(NovaSenhaRequest request);
        Task<MensagemResponse> EnviarCodigoResetSenha(ResetSenhaRequest request);
    }
}
