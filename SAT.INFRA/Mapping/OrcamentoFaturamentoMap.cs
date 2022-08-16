using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoFaturamentoMap : IEntityTypeConfiguration<OrcamentoFaturamento>
    {
        public void Configure(EntityTypeBuilder<OrcamentoFaturamento> builder)
        {
            builder
                .ToTable("OrcamentoFaturamento");

            builder
                .HasKey(i => i.CodOrcamentoFaturamento);
        }
    }
}