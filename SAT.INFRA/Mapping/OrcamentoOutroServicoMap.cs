using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoOutroServicoMap : IEntityTypeConfiguration<OrcamentoOutroServico>
    {
        public void Configure(EntityTypeBuilder<OrcamentoOutroServico> builder)
        {
            builder
                .ToTable("OrcOutroServico");

            builder
                .HasKey(prop => prop.CodOrcOutroServico);
        }
    }
}