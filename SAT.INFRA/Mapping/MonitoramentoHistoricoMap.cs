using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MonitoramentoHistoricoMap : IEntityTypeConfiguration<MonitoramentoHistorico>
    {
        public void Configure(EntityTypeBuilder<MonitoramentoHistorico> builder)
        {
            builder
                .ToTable("HistLogAlerta");

            builder
                .HasKey(i => i.CodHistLogAlerta);
        }
    }
}