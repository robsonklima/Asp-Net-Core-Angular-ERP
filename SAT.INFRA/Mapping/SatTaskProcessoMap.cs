using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class SatTaskProcessoMap : IEntityTypeConfiguration<SatTaskProcesso>
    {
        public void Configure(EntityTypeBuilder<SatTaskProcesso> builder)
        {
            builder.ToTable("SatTaskProcesso");
            builder.HasKey(i => i.CodSatTaskProcesso);
            builder
                .HasOne(i => i.Tipo)
                .WithMany()
                .HasForeignKey(prop => prop.CodSatTaskTipo)
                .HasPrincipalKey(prop => prop.CodSatTaskTipo);            
        }
    }
}