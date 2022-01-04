using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoDeslocamentoMap : IEntityTypeConfiguration<OrcamentoDeslocamento>
    {
        public void Configure(EntityTypeBuilder<OrcamentoDeslocamento> builder)
        {
            builder
                .ToTable("OrcDeslocamento");

            builder
                .HasKey(prop => prop.CodOrcDeslocamento);
        }
    }
}