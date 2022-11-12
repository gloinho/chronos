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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository {
        public UsuarioRepository(ApplicationDbContext manutencaoContext) : base(manutencaoContext) {

        }

        public Task<bool> Confirmar(string token) {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetPorEmail(string email) {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetPorToken(string token) {
            throw new NotImplementedException();
        }
    }
}
