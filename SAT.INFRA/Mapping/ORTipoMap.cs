using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORTipoMap : IEntityTypeConfiguration<ORTipo>
    {
        public void Configure(EntityTypeBuilder<ORTipo> builder)
        {
            builder.ToTable("TipoOR");
            builder.HasKey(i => i.CodTipoOR);
        }
    }
}