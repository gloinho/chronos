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

        public override async Task<ICollection<Projeto>> ObterTodosAsync()
        {
            return await base._context
                .Set<Projeto>()
                .Include(p => p.Usuarios)
                .ThenInclude(p => p.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Projeto> ObterPorIdAsync(int id)
        {
            return await base._context
                .Set<Projeto>()
                .Include(p => p.Usuarios)
                .ThenInclude(p => p.Usuario)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
