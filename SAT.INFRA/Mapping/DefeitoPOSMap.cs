using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DefeitoPOSMap : IEntityTypeConfiguration<DefeitoPOS>
    {
        public void Configure(EntityTypeBuilder<DefeitoPOS> builder)
        {
            builder.ToTable("DefeitoPOS");
            builder.HasKey(prop => prop.CodDefeitoPOS);
            builder.Property(prop => prop.NomeDefeitoPOS).HasColumnName("DefeitoPOS");
        }
    }
}
