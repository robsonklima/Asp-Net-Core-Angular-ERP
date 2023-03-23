using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class RelatorioAtendimentoDetalhePecaStatusMap : IEntityTypeConfiguration<RelatorioAtendimentoDetalhePecaStatus>
    {
        public void Configure(EntityTypeBuilder<RelatorioAtendimentoDetalhePecaStatus> builder)
        {
            builder.
                ToTable("RelatorioAtendimentoDetalhePecaStatus");

            builder.
                HasKey(i => new { i.CodRATDetalhesPecasStatus });

            builder
                .HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioCad)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.RelatorioAtendimentoPecaStatus)
                .WithMany()
                .HasForeignKey(prop => prop.CodRatPecasStatus)
                .HasPrincipalKey(prop => prop.CodRatpecasStatus);

        }
    }
}