using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TipoFreteMap : IEntityTypeConfiguration<TipoFrete>
    {
        public void Configure(EntityTypeBuilder<TipoFrete> builder)
        {
            builder
                .ToTable("TipoFrete");

            builder
                .HasKey(i => i.CodTipoFrete);
        }
    }
}