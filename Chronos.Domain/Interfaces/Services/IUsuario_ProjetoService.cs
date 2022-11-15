using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IUsuario_ProjetoService
    {
        Task CadastrarAsync(Usuario_Projeto relacao);
        Task<Usuario_Projeto> CheckSeUsuarioFazParteDoProjeto(int id);
        Task CheckPermissao(int usuario_projetoId);
        Task<Usuario_Projeto> CheckSePodeAlterarTarefa(int projetoId, Tarefa tarefa);
    }
}
