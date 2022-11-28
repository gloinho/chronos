using Chronos.Data.Context;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;

namespace Chronos.Data.Repositories
{
    public class ProjetoRepository : BaseRepository<Projeto>, IProjetoRepository
    {
        public ProjetoRepository(ApplicationDbContext manutencaoContext) : base(manutencaoContext)
        { }
    }
}
