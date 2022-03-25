using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AcordoNivelServicoLegadoMap : IEntityTypeConfiguration<AcordoNivelServicoLegado>
    {
        public void Configure(EntityTypeBuilder<AcordoNivelServicoLegado> builder)
        {
            builder
                .ToTable("SLA");

            builder
                .HasKey(prop => prop.CodSla);
        }
    }
}