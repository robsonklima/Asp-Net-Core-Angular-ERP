using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MonitoramentoMap : IEntityTypeConfiguration<Monitoramento>
    {
        public void Configure(EntityTypeBuilder<Monitoramento> builder)
        {
            builder.ToTable("LogAlerta");
            builder.HasKey(i => i.CodLogAlerta);
        }
    }
}