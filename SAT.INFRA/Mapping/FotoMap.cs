using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class FotoMap : IEntityTypeConfiguration<Foto>
    {
        public void Configure(EntityTypeBuilder<Foto> builder)
        {
            builder.ToTable("RatFotoSmartphone");
            builder.HasKey(i => i.CodRATFotoSmartphone);
            builder.Ignore(i => i.Base64);
        }
    }
}
