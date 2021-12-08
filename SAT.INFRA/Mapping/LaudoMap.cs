using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LaudoMap : IEntityTypeConfiguration<Laudo>
    {
        public void Configure(EntityTypeBuilder<Laudo> builder)
        {
            builder
                .ToTable("Laudo");

            builder
                .HasKey(prop => prop.CodLaudo);

            builder
                .HasMany(p => p.LaudosSituacao)
                .WithOne()
                .HasForeignKey(p => p.CodLaudo)
                .HasPrincipalKey(p => p.CodLaudo);

            builder
                .HasOne(p => p.LaudoStatus)
                .WithOne()
                .HasForeignKey<Laudo>("CodLaudoStatus")
                .HasPrincipalKey<LaudoStatus>("CodLaudoStatus");
        }
    }
}