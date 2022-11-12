using Chronos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.Data.Mappings;

public class TarefaMap : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder
            .HasOne(prop => prop.Usuario_Projeto)
            .WithMany(prop => prop.Tarefas)
            .HasForeignKey(prop => prop.Usuario_ProjetoId);
    }
}
