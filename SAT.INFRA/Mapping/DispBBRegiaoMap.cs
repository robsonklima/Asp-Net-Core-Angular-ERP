using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DispBBRegiaoMap : IEntityTypeConfiguration<DispBBRegiao>
    {
        public void Configure(EntityTypeBuilder<DispBBRegiao> builder)
        {
            builder
                .ToTable("DispBBRegiao");

            builder
                .HasKey(prop => prop.CodDispBBRegiao);

            builder
                .HasOne(prop => prop.DispBBPercRegiao)
                .WithMany()
                .HasForeignKey(prop => prop.CodDispBBRegiao)
                .HasPrincipalKey(prop => prop.CodDispBBRegiao);

        }
    }
}