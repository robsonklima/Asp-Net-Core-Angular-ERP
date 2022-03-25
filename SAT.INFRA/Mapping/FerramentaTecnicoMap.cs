using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class FerramentaTecnicoMap : IEntityTypeConfiguration<FerramentaTecnico>
    {
        public void Configure(EntityTypeBuilder<FerramentaTecnico> builder)
        {
            builder
                .ToTable("FerramentaTecnico");

            builder
                .HasKey(i => i.CodFerramentaTecnico);
        }
    }
}