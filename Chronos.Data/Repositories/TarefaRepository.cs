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
<<<<<<< HEAD
            var tarefa = await _context.Tarefas
                .Where(p => p.Id == id)
                .Include(p => p.Usuario_Projeto)
                .ThenInclude(u => u.Projeto)
                .Include(p => p.Usuario_Projeto)
                .ThenInclude(p => p.Usuario)
                .FirstOrDefaultAsync();
            return tarefa;
        }

        public override async Task<ICollection<Tarefa>> ObterTodosAsync()
        {
            var tarefas = await _context.Tarefas
                .Include(p => p.Usuario_Projeto)
                .ThenInclude(u => u.Projeto)
                .Include(p => p.Usuario_Projeto)
                .ThenInclude(u => u.Usuario)
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Tarefa>> ObterPorUsuarioIdAsync(int usuarioId)
        {
            var tarefas = await _context.Usuarios_Projetos
=======
            var tarefas = await base._context.Usuarios_Projetos
>>>>>>> origin/main
                .Where(up => up.UsuarioId == usuarioId)
                .Include(up => up.Projeto)
                .Include(up => up.Tarefas)
<<<<<<< HEAD
                .ThenInclude(t => t.Usuario_Projeto)
                .ThenInclude(up => up.Projeto)
                .Include(up => up.Usuario)
                .SelectMany(t => t.Tarefas)
=======
                .Where(t => t.DataInclusao == DateTime.Today)
>>>>>>> origin/main
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Usuario_Projeto>> GetTarefasMes(int usuarioId)
        {
            var tarefas = await base._context.Usuarios_Projetos
                .Where(up => up.UsuarioId == usuarioId)
                .Include(up => up.Projeto)
                .Include(up => up.Tarefas)
<<<<<<< HEAD
                .ThenInclude(t => t.Usuario_Projeto)
                .ThenInclude(up => up.Projeto)
                .Include(up => up.Usuario)
                .SelectMany(
                    t =>
                        t.Tarefas.Where(
                            t =>
                                t.DataInicial.Value.Date == DateTime.Today
                                && DateTime.Today == t.DataFinal.Value.Date
                        )
                )
=======
                .Where(t => t.DataInclusao.Month == DateTime.Today.Month)
>>>>>>> origin/main
                .ToListAsync();
            return tarefas;
        }

        public async Task<List<Usuario_Projeto>> GetTarefasProjeto(int projetoId)
        {
            var tarefas = await base._context.Usuarios_Projetos
                .Where(up => up.ProjetoId == projetoId)
                .Include(up => up.Projeto)
                .Include(up => up.Tarefas)
<<<<<<< HEAD
                .ThenInclude(t => t.Usuario_Projeto)
                .ThenInclude(up => up.Projeto)
                .Include(up => up.Usuario)
                .SelectMany(
                    t =>
                        t.Tarefas.Where(
                            t =>
                                EF.Functions.DateDiffWeek(DateTime.Today, t.DataInicial.Value.Date)
                                    == 0
                                && EF.Functions.DateDiffWeek(DateTime.Today, t.DataFinal.Value.Date)
                                    == 0
                        )
                )
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
                .Include(up => up.Usuario)
                .SelectMany(
                    t =>
                        t.Tarefas.Where(
                            t =>
                                EF.Functions.DateDiffMonth(DateTime.Today, t.DataInicial.Value.Date)
                                    == 0
                                && EF.Functions.DateDiffMonth(
                                    DateTime.Today,
                                    t.DataFinal.Value.Date
                                ) == 0
                        )
                )
=======
>>>>>>> origin/main
                .ToListAsync();
            return tarefas;
        }

        public Task<List<Usuario_Projeto>> GetTarefasSemana(int usuarioId)
        {
<<<<<<< HEAD
            var tarefas = await _context.Usuarios_Projetos
                .Where(u => u.ProjetoId == projetoId)
                .Include(p => p.Tarefas)
                .ThenInclude(p => p.Usuario_Projeto)
                .ThenInclude(p => p.Usuario)
                .SelectMany(p => p.Tarefas)
                .ToListAsync();
            return tarefas;
=======
            throw new NotImplementedException();
>>>>>>> origin/main
        }
    }
}
