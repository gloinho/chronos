using Chronos.Data.Context;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext manutencaoContext) : base(manutencaoContext)
        { }

        public async Task Confirmar(Usuario usuario)
        {
            usuario.Confirmado = true;
            await AlterarAsync(usuario);
        }
    }
}
