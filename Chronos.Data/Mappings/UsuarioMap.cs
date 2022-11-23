using Chronos.Domain.Entities;
using Chronos.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.Data.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasMany(prop => prop.Projetos).WithOne(prop => prop.Usuario);
        builder.HasIndex(prop => prop.Email).IsUnique();
        builder
            .Property(prop => prop.Permissao)
            .HasConversion(
                prop => prop.ToString(),
                prop => (Permissao)Enum.Parse(typeof(Permissao), prop)
            );
    }
}
