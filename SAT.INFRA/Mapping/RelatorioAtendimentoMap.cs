using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class RelatorioAtendimentoMap : IEntityTypeConfiguration<RelatorioAtendimento>
    {
        public void Configure(EntityTypeBuilder<RelatorioAtendimento> builder)
        {
            builder.ToTable("RAT");
            builder.HasKey(i => new { i.CodRAT });

            builder
                .HasMany(i => i.Fotos)
                .WithOne()
                .HasForeignKey("NumRAT")
                .HasPrincipalKey("NumRAT");

            builder
                .HasMany(i => i.Laudos)
                .WithOne()
                .HasForeignKey(i => i.CodRAT);

            builder
                .HasMany(i => i.RelatorioAtendimentoDetalhes)
                .WithOne()
                .HasForeignKey(i => i.CodRAT);

            builder
                .HasMany(i => i.ProtocolosSTN)
                .WithOne()
                .HasForeignKey(i => i.CodRAT);

            builder
                .HasMany(i => i.CheckinsCheckouts)
                .WithOne()
                .HasForeignKey(i => i.CodRAT);

            builder
                .HasOne(i => i.StatusServico)
                .WithOne()
                .HasForeignKey<RelatorioAtendimento>(i => i.CodStatusServico)
                .HasPrincipalKey<StatusServico>(i => i.CodStatusServico);

            builder
                .HasOne(i => i.Tecnico)
                .WithOne()
                .HasForeignKey<RelatorioAtendimento>(i => i.CodTecnico)
                .HasPrincipalKey<Tecnico>(i => i.CodTecnico);

            builder
                .HasOne(i => i.TipoServico)
                .WithOne()
                .HasForeignKey<RelatorioAtendimento>(i => i.CodServico)
                .HasPrincipalKey<TipoServico>(i => i.CodServico);
        }
    }
}