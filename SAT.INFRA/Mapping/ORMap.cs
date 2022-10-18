using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORMap : IEntityTypeConfiguration<OR>
    {
        public void Configure(EntityTypeBuilder<OR> builder)
        {
            builder.ToTable("OR");
            builder.HasKey(i => i.CodOR);
        }
    }
}