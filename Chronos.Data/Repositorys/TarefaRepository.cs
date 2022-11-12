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
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository {
        public TarefaRepository(ApplicationDbContext manutencaoContext) : base(manutencaoContext) {

        }

        public Task<Tarefa> GetTarefasDia(int usuarioId) {
            throw new NotImplementedException();
        }

        public Task<Tarefa> GetTarefasMes(int usuarioId) {
            throw new NotImplementedException();
        }

        public Task<Tarefa> GetTarefasProjeto(int projetoId) {
            throw new NotImplementedException();
        }

        public Task<Tarefa> GetTarefasSemana(int usuarioId) {
            throw new NotImplementedException();
        }
    }
}
