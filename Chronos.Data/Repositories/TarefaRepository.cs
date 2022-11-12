using Chronos.Data.Context;
using Chronos.Domain.Entities;
using Chronos.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Data.Repositories
{
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(ApplicationDbContext manutencaoContext) : base(manutencaoContext)
        { }

        public async Task<List<Usuario_Projeto>> GetTarefasDia(int usuarioId)
        {
            var tarefas = await base._context.Usuarios_Projetos
                .Where(up => up.UsuarioId == usuarioId)
                .Include(up => up.Projeto)
                .Include(up => up.Tarefas)
                .Where(t => t.DataInclusao == DateTime.Today)
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Usuario_Projeto>> GetTarefasMes(int usuarioId)
        {
            var tarefas = await base._context.Usuarios_Projetos
                .Where(up => up.UsuarioId == usuarioId)
                .Include(up => up.Projeto)
                .Include(up => up.Tarefas)
                .Where(t => t.DataInclusao.Month == DateTime.Today.Month)
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Usuario_Projeto>> GetTarefasProjeto(int projetoId)
        {
            var tarefas = await base._context.Usuarios_Projetos
                .Where(up => up.ProjetoId == projetoId)
                .Include(up => up.Projeto)
                .Include(up => up.Tarefas)
                .ToListAsync();
            return tarefas;
        }

        public Task<List<Usuario_Projeto>> GetTarefasSemana(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
