using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LogAlertaMap : IEntityTypeConfiguration<LogAlerta>
    {
        public void Configure(EntityTypeBuilder<LogAlerta> builder)
        {
            builder
                .ToTable("LogAlerta");

            builder
                .HasKey(i => i.CodLogAlerta);
        }
    }
}