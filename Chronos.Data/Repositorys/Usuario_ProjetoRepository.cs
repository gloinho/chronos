using Chronos.Data.Context;
using Chronos.Data.Repositories;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Data.Repository {
    public class Usuario_ProjetoRepository : BaseRepository<Usuario_Projeto>, IUsuario_ProjetoRepository {
        public Usuario_ProjetoRepository(ApplicationDbContext manutencaoContext) : base(manutencaoContext) {

        }
    }
}
