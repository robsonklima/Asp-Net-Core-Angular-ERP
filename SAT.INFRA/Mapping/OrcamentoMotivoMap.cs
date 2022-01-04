using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoMotivoMap : IEntityTypeConfiguration<OrcamentoMotivo>
    {
        public void Configure(EntityTypeBuilder<OrcamentoMotivo> builder)
        {
            builder
                .ToTable("OrcMotivo");

            builder
                .HasKey(prop => prop.CodOrcMotivo);
        }
    }
}