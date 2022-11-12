using Chronos.Data.Context;
using Chronos.Data.Repositories;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;

namespace Chronos.Data.Repositories
{
    public class Usuario_ProjetoRepository
        : BaseRepository<Usuario_Projeto>,
            IUsuario_ProjetoRepository
    {
        public Usuario_ProjetoRepository(ApplicationDbContext manutencaoContext)
            : base(manutencaoContext) { }
    }
}
