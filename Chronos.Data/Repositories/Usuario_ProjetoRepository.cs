using Chronos.Data.Context;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Data.Repositories
{
    public class Usuario_ProjetoRepository
        : BaseRepository<Usuario_Projeto>,
            IUsuario_ProjetoRepository
    {
        public Usuario_ProjetoRepository(ApplicationDbContext manutencaoContext)
            : base(manutencaoContext) { }

        public async Task<Usuario_Projeto> ObterPorUsuarioIdProjetoId(int projetoId, int? usuarioId)
        {
            return await _context.Usuarios_Projetos.FirstOrDefaultAsync(
                p => p.ProjetoId == projetoId && p.UsuarioId == usuarioId
            );
        }
    }
}
