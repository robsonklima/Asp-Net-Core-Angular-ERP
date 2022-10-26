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

            builder
                .HasMany(prop => prop.ORItens)
                .WithOne()
                .HasForeignKey(prop => prop.CodOR)
                .HasPrincipalKey(prop => prop.CodOR);

            builder
                .HasOne(prop => prop.ORStatus)
                .WithMany()
                .HasForeignKey(prop => prop.CodStatusOR)
                .HasPrincipalKey(prop => prop.CodStatus);
        }
    }
}