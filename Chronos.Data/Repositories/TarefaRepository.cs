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

        public override async Task<Tarefa> ObterPorIdAsync(int id)
        {
            var tarefa = await _context.Tarefas
                .Where(p => p.Id == id)
                .Include(p => p.Usuario_Projeto)
                .ThenInclude(u => u.Projeto)
                .FirstOrDefaultAsync();
            return tarefa;
        }

        public override async Task<ICollection<Tarefa>> ObterTodosAsync()
        {
            var tarefas = await _context.Tarefas
                .Include(p => p.Usuario_Projeto)
                .ThenInclude(u => u.Projeto)
                .AsNoTracking()
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Tarefa>> ObterPorUsuarioIdAsync(int usuarioId)
        {
            var tarefas = await _context.Usuarios_Projetos
                .Where(up => up.UsuarioId == usuarioId)
                .Include(up => up.Tarefas)
                .ThenInclude(t => t.Usuario_Projeto)
                .ThenInclude(up => up.Projeto)
                .SelectMany(t => t.Tarefas)
                .AsNoTracking()
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Tarefa>> GetTarefasDia(int usuarioId)
        {
            var tarefas = await _context.Usuarios_Projetos
                .Where(up => up.UsuarioId == usuarioId)
                .Include(up => up.Tarefas)
                .ThenInclude(t => t.Usuario_Projeto)
                .ThenInclude(up => up.Projeto)
                .SelectMany(t => t.Tarefas.Where(t => t.DataInclusao.Day == DateTime.Today.Day))
                .AsNoTracking()
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Tarefa>> GetTarefasSemana(int usuarioId)
        {
            var tarefas = await _context.Usuarios_Projetos
                .Where(up => up.UsuarioId == usuarioId)
                .Include(up => up.Tarefas)
                .ThenInclude(t => t.Usuario_Projeto)
                .ThenInclude(up => up.Projeto)
                .SelectMany(
                    t =>
                        t.Tarefas.Where(
                            t => EF.Functions.DateDiffWeek(DateTime.Today, t.DataInclusao) == 0
                        )
                )
                .AsNoTracking()
                .ToListAsync();

            return tarefas;
        }

        public async Task<List<Tarefa>> GetTarefasMes(int usuarioId)
        {
            var tarefas = await _context.Usuarios_Projetos
                .Where(up => up.UsuarioId == usuarioId)
                .Include(up => up.Tarefas)
                .ThenInclude(t => t.Usuario_Projeto)
                .ThenInclude(up => up.Projeto)
                .SelectMany(t => t.Tarefas.Where(t => t.DataInclusao.Month == DateTime.Today.Month))
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Tarefa>> GetTarefasProjeto(int projetoId)
        {
            var tarefas = await _context.Usuarios_Projetos
                .Where(u => u.ProjetoId == projetoId)
                .Include(p => p.Tarefas)
                .ThenInclude(t => t.Usuario_Projeto)
                .ThenInclude(up => up.Projeto)
                .SelectMany(t => t.Tarefas)
                .AsNoTracking()
                .ToListAsync();
            return tarefas;
        }
    }
}
