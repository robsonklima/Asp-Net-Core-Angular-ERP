using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class RelatorioAtendimentoDetalheMap : IEntityTypeConfiguration<RelatorioAtendimentoDetalhe>
    {
        public void Configure(EntityTypeBuilder<RelatorioAtendimentoDetalhe> builder)
        {
            builder.ToTable("RATDetalhes");
            builder.HasKey(i => new { i.CodRATDetalhe });

            builder
                .HasOne(i => i.TipoCausa)
                .WithOne()
                .HasForeignKey<RelatorioAtendimentoDetalhe>(i => i.CodTipoCausa)
                .HasPrincipalKey<TipoCausa>(i => i.CodTipoCausa);

            builder
                .HasOne(i => i.GrupoCausa)
                .WithOne()
                .HasForeignKey<RelatorioAtendimentoDetalhe>(i => i.CodGrupoCausa)
                .HasPrincipalKey<GrupoCausa>(i => i.CodGrupoCausa);

            builder
                .HasOne(i => i.Defeito)
                .WithOne()
                .HasForeignKey<RelatorioAtendimentoDetalhe>(i => i.CodDefeito)
                .HasPrincipalKey<Defeito>(i => i.CodDefeito);

            builder
                .HasOne(i => i.Causa)
                .WithOne()
                .HasForeignKey<RelatorioAtendimentoDetalhe>(i => i.CodCausa)
                .HasPrincipalKey<Causa>(i => i.CodCausa);
        }
    }
}