using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AcordoNivelServicoMap : IEntityTypeConfiguration<AcordoNivelServico>
    {
        public void Configure(EntityTypeBuilder<AcordoNivelServico> builder)
        {
            builder
                .ToTable("SLA_NEW");

            builder
                .HasKey(prop => prop.CodSLA);
        }
    }
}