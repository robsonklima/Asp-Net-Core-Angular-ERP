using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MoedaMap : IEntityTypeConfiguration<Moeda>
    {
        public void Configure(EntityTypeBuilder<Moeda> builder)
        {
            builder
                .ToTable("Moeda");

            builder
                .HasKey(i => i.CodMoeda);
        }
    }
}