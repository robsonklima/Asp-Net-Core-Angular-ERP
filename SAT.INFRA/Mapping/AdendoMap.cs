using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AdendoMap : IEntityTypeConfiguration<Adendo>
    {
        public void Configure(EntityTypeBuilder<Adendo> builder)
        {
            builder.ToTable("Adendo");
            builder.HasKey(i => i.CodAdendo);

            builder
                .HasMany(prop => prop.Itens)
                .WithOne()
                .HasForeignKey(prop => prop.CodAdendo)
                .HasPrincipalKey(prop => prop.CodAdendo);
        }
    }
}