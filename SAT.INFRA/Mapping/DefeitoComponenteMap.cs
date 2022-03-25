using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DefeitoComponenteMap : IEntityTypeConfiguration<DefeitoComponente>
    {
        public void Configure(EntityTypeBuilder<DefeitoComponente> builder)
        {
            builder
                .ToTable("DefeitoComponente");

            builder
                .HasKey(i => i.CodDefeitoComponente);

            builder
                .HasOne(i => i.Causa)
                .WithMany()
                .HasForeignKey("CodECausa")
                .HasPrincipalKey("CodECausa");

            builder
                .HasOne(i => i.Defeito)
                .WithMany()
                .HasForeignKey("CodDefeito")
                .HasPrincipalKey("CodDefeito");
        }
    }
}