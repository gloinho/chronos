

using Chronos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chronos.Data.Mappings
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder
                .HasOne(prop => prop.Responsavel)
                .WithMany(prop => prop.Logs)
                .HasForeignKey(prop => prop.ResponsavelId);

        }
    }
}
