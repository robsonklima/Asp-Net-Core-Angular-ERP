using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoMaoDeObraMap : IEntityTypeConfiguration<OrcamentoMaoDeObra>
    {
        public void Configure(EntityTypeBuilder<OrcamentoMaoDeObra> builder)
        {
            builder
                .ToTable("OrcMaoObra");

            builder
                .HasKey(prop => prop.CodOrcMaoObra);
        }
    }
}