using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Repository
{
    public interface IUsuario_ProjetoRepository : IBaseRepository<Usuario_Projeto>
    {
        Task<Usuario_Projeto> ObterPorUsuarioIdProjetoId(int projetoId, int usuarioId);
    }
}
