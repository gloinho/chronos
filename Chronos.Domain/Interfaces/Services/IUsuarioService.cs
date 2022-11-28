using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
using Chronos.Domain.Entities.Enums;
using Chronos.Domain.Utils;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<UsuarioRequest, UsuarioResponse>
    {
<<<<<<< HEAD
        Task<MensagemResponse> AlterarSenha(NovaSenhaRequest request);
        Task<MensagemResponse> EnviarCodigoResetSenha(ResetSenhaRequest request);
        Task<MensagemResponse> MudarPermissao(int id, Permissao permissao);
=======
>>>>>>> origin/main
    }
}
