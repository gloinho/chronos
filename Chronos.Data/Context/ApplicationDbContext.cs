using Chronos.Data.Mappings;
using Chronos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Data.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Usuario_Projeto> Usuarios_Projetos { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public ApplicationDbContext()
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(new UsuarioMap().Configure);
        modelBuilder.Entity<Projeto>(new ProjetoMap().Configure);
        modelBuilder.Entity<Tarefa>(new TarefaMap().Configure);
        modelBuilder.Entity<Usuario_Projeto>(new Usuario_ProjetoMap().Configure);
    }
}
