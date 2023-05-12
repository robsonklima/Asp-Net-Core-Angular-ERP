using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class RedeBanrisulMap : IEntityTypeConfiguration<RedeBanrisul>
    {
        public void Configure(EntityTypeBuilder<RedeBanrisul> builder)
        {
            builder.ToTable("RedeBanrisul");

            builder.HasKey(prop => prop.CodRedeBanrisul);
            builder.Property(p => p.Rede).HasColumnName("RedeBanrisul");
        }
    }
}
