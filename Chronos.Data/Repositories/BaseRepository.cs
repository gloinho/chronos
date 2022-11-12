using Chronos.Data.Context;
using Chronos.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        internal readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> Cadastrar(T entidade)
        {
            await _context.Set<T>().AddAsync(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        public async Task<T> Editar(T entidade)
        {
            _context.Set<T>().Update(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        public async Task<T> Excluir(T entidade)
        {
            _context.Set<T>().Remove(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        public async Task<T> GetPorId(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<ICollection<T>> Listar()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
    }
}
