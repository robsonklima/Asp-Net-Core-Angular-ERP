using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AuditoriaVeiculoAcessorioMap : IEntityTypeConfiguration<AuditoriaVeiculoAcessorio>
    {
        public void Configure(EntityTypeBuilder<AuditoriaVeiculoAcessorio> builder)
        {
            builder
                .ToTable("AuditoriaVeiculoAcessorio");

            builder
                .HasKey(i => i.CodAuditoriaVeiculoAcessorio);
        }
    }
}