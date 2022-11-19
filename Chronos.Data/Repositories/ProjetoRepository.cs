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
            var projetos = await _context.Usuarios_Projetos
                .Where(up => up.UsuarioId == usuarioId)
                .Include(p => p.Projeto)
                .Select(p => p.Projeto)
                .ToListAsync();

            return projetos;
        }
    }
}
