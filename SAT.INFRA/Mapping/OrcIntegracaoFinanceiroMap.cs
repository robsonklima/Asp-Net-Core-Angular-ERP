using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcIntegracaoFinanceiroMap : IEntityTypeConfiguration<OrcIntegracaoFinanceiro>
    {
        public void Configure(EntityTypeBuilder<OrcIntegracaoFinanceiro> builder)
        {
            builder.ToTable("OrcIntegracaoFinanceiro");
            builder.HasKey(i => i.CodOrcIntegracaoFinanceiro);
        }
    }
}