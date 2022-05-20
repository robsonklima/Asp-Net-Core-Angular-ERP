using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DispBBBloqueioOSMap : IEntityTypeConfiguration<DispBBBloqueioOS>
    {
        public void Configure(EntityTypeBuilder<DispBBBloqueioOS> builder)
        {
            builder.ToTable("DispBBBloqueioOS");
            builder.HasKey(prop => new { prop.CodDispBBBloqueioOS, prop.CodOS });
        }
    }
}
