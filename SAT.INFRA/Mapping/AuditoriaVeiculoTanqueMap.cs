using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AuditoriaVeiculoTanqueMap : IEntityTypeConfiguration<AuditoriaVeiculoTanque>
    {
        public void Configure(EntityTypeBuilder<AuditoriaVeiculoTanque> builder)
        {
            builder
                .ToTable("AuditoriaVeiculoTanque");

            builder
                .HasKey(i => i.CodAuditoriaVeiculoTaque);
        }
    }
}