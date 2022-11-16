using Chronos.Data.Context;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Data.Repositories
{
    public class ProjetoRepository : BaseRepository<Projeto>, IProjetoRepository
    {
        public ProjetoRepository(ApplicationDbContext manutencaoContext) : base(manutencaoContext)
        { }

        public async Task<List<Projeto>> ObterPorUsuarioIdAsync(int usuarioId)
        {
            var projetos = await _context.Projetos
                .Include(p => p.Usuarios.Where(up => up.UsuarioId == usuarioId))
                .ToListAsync();
            return projetos;
        }
    }
}
