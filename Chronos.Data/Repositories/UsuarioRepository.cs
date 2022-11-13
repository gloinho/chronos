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

        public async Task<Usuario> GetPorEmail(string email)
        {
            return await base._context.Usuarios.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<Usuario> GetPorToken(string token)
        {
            return await base._context.Usuarios.FirstOrDefaultAsync(user => user.ConfirmacaoToken == token);
        }
    }
}
