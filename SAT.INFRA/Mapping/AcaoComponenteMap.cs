using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AcaoComponenteMap : IEntityTypeConfiguration<AcaoComponente>
    {
        public void Configure(EntityTypeBuilder<AcaoComponente> builder)
        {
            builder
                .ToTable("AcaoComponente");

            builder
                .HasKey(i => i.CodAcaoComponente);

            builder
                .HasOne(i => i.Causa)
                .WithMany()
                .HasForeignKey("CodECausa")
                .HasPrincipalKey("CodECausa");

            builder
                .HasOne(i => i.Acao)
                .WithMany()
                .HasForeignKey("CodAcao")
                .HasPrincipalKey("CodAcao");
        }
    }
}