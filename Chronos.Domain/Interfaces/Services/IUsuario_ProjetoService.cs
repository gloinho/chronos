using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Services
{
    public interface IUsuario_ProjetoService
    {
        Task CadastrarAsync(int projetoId, int usuarioId);
        Task<Usuario_Projeto> CheckSeUsuarioFazParteDoProjeto(int projetoId, int? usuarioId);
        Task CheckPermissaoRelacao(int usuario_projetoId);
        Task CheckPermissao(int usuarioId);
        Task<Usuario_Projeto> CheckSePodeAlterarTarefa(int projetoId, Tarefa tarefa);
        Task CheckSeProjetoExiste(int id);
        Task CheckSeUsuarioExiste(int usuarioId);
        Task InativarColaborador(int projetoId, int usuarioId);
    }
}
