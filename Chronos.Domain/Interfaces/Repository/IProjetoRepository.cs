using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Repository
{
    public interface IProjetoRepository : IBaseRepository<Projeto>
    {
        Task<List<Projeto>> ObterPorUsuarioIdAsync(int usuarioId);
    }
}
