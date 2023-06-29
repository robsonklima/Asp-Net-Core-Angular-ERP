using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ANSMap : IEntityTypeConfiguration<ANS>
    {
        public void Configure(EntityTypeBuilder<ANS> builder)
        {
            builder.ToTable("ANS");
            builder.HasKey(i => new { i.CodANS });
        }
    }
}