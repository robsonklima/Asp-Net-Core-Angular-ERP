using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcFormaPagamentoMap : IEntityTypeConfiguration<OrcFormaPagamento>
    {
        public void Configure(EntityTypeBuilder<OrcFormaPagamento> builder)
        {
            builder.ToTable("OrcFormaPagamento");
            builder.HasKey(i => i.CodOrcFormaPagamento);
        }
    }
}