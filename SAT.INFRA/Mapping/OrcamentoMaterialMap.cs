using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoMaterialMap : IEntityTypeConfiguration<OrcamentoMaterial>
    {
        public void Configure(EntityTypeBuilder<OrcamentoMaterial> builder)
        {
            builder
                .ToTable("OrcMaterial");

            builder
                .HasKey(prop => prop.CodOrcMaterial);
        }
    }
}