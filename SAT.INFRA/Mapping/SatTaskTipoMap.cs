using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class SatTaskTipoMap : IEntityTypeConfiguration<SatTaskTipo>
    {
        public void Configure(EntityTypeBuilder<SatTaskTipo> builder)
        {
            builder.ToTable("SatTaskTipo");
            builder.HasKey(i => i.CodSatTaskTipo);
        }
    }
}