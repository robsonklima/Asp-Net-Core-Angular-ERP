using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORTransporteMap : IEntityTypeConfiguration<ORTransporte>
    {
        public void Configure(EntityTypeBuilder<ORTransporte> builder)
        {
            builder.ToTable("ORTransporte");
            builder.HasKey(i => i.CodTransportadora);
        }
    }
}