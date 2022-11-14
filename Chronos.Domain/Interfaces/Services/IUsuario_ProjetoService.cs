using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IUsuario_ProjetoService
    {
        Task CadastrarAsync(Usuario_Projeto relacao);
    }
}
