using Chronos.Domain.Entities;

namespace Chronos.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> GetPorEmail(string email);
        Task<Usuario> GetPorToken(string token);
        Task Confirmar(string token);
    }
}
