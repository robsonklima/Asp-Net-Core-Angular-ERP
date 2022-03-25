using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class CausaMap : IEntityTypeConfiguration<Causa>
    {
        public void Configure(EntityTypeBuilder<Causa> builder)
        {
            builder
                .ToTable("Causa");

            builder
                .HasKey(i => i.CodCausa);
        }
    }
}