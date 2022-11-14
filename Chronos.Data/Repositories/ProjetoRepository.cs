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
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
