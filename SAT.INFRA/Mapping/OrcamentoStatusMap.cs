using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoStatusMap : IEntityTypeConfiguration<OrcamentoStatus>
    {
        public void Configure(EntityTypeBuilder<OrcamentoStatus> builder)
        {
            builder
                .ToTable("OrcStatus");

            builder
                .HasKey(prop => prop.CodOrcStatus);
        }
    }
}