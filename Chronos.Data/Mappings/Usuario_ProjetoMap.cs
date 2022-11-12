using Chronos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.Data.Mappings;

public class Usuario_ProjetoMap : IEntityTypeConfiguration<Usuario_Projeto>
{
    public void Configure(EntityTypeBuilder<Usuario_Projeto> builder)
    {
        builder
            .HasOne(prop => prop.Usuario)
            .WithMany(prop => prop.Projetos)
            .HasForeignKey(prop => prop.UsuarioId);

        builder
            .HasOne(prop => prop.Projeto)
            .WithMany(prop => prop.Usuarios)
            .HasForeignKey(prop => prop.ProjetoId);
    }
}
