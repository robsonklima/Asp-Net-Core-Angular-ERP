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

            builder
                .HasOne(prop => prop.Tecnico)
                .WithMany()
                .HasForeignKey(prop => prop.CodTecnico)
                .HasPrincipalKey(prop => prop.CodTecnico);

            builder
                .HasOne(prop => prop.Or)
                .WithMany()
                .HasForeignKey(prop => prop.CodOS)
                .HasPrincipalKey(prop => prop.CodOS);

            builder
                .HasOne(prop => prop.Rat)
                .WithMany()
                .HasForeignKey(prop => prop.CodRAT)
                .HasPrincipalKey(prop => prop.CodRAT);
        }
    }
}