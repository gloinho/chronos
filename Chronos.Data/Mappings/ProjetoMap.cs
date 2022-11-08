using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.Data.Mappings;

public class ProjetoMap : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
          builder.HasMany(prop => prop.Usuarios).WithOne(prop => prop.Projeto);
    }
}
