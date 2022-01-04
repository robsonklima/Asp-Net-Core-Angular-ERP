using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoDescontoMap : IEntityTypeConfiguration<OrcamentoDesconto>
    {
        public void Configure(EntityTypeBuilder<OrcamentoDesconto> builder)
        {
            builder
                .ToTable("OrcDesconto");

            builder
                .HasKey(prop => prop.CodOrcDesconto);
        }
    }
}