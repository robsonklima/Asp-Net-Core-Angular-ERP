using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoMap : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder)
        {
            builder
                .ToTable("Orc");

            builder
                .HasKey(prop => prop.CodOrc);

            builder
                .HasOne(i => i.OrdemServico)
                .WithOne()
                .HasForeignKey<OrdemServico>(i => i.CodOS)
                .HasPrincipalKey<Orcamento>(i => i.CodigoOrdemServico);

            builder
                .HasOne(i => i.OrcamentoMotivo)
                .WithMany()
                .HasForeignKey(i => i.CodigoMotivo)
                .HasPrincipalKey(i => i.CodOrcMotivo);

            builder
                .HasOne(i => i.Cliente)
                .WithMany()
                .HasForeignKey(i => i.CodigoCliente)
                .HasPrincipalKey(i => i.CodCliente);

            builder
                .HasOne(i => i.Filial)
                .WithMany()
                .HasForeignKey(i => i.CodigoFilial)
                .HasPrincipalKey(i => i.CodFilial);                

            builder
               .HasMany(i => i.Materiais)
               .WithOne()
               .HasForeignKey(i => i.CodOrc)
               .HasPrincipalKey(i => i.CodOrc)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(i => i.MaoDeObra)
                .WithOne()
                .HasForeignKey<OrcamentoMaoDeObra>(i => i.CodOrc)
                .HasPrincipalKey<Orcamento>(i => i.CodOrc)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(i => i.OrcamentoDeslocamento)
                .WithOne()
                .HasForeignKey<OrcamentoDeslocamento>(i => i.CodOrc)
                .HasPrincipalKey<Orcamento>(i => i.CodOrc)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasMany(i => i.OutrosServicos)
               .WithOne()
               .HasForeignKey(i => i.CodOrc)
               .HasPrincipalKey(i => i.CodOrc)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasMany(i => i.Descontos)
               .WithOne()
               .HasForeignKey(i => i.CodOrc)
               .HasPrincipalKey(i => i.CodOrc)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(i => i.OrcamentoStatus)
                .WithMany()
                .HasForeignKey(i => i.CodigoStatus)
                .HasPrincipalKey(i => i.CodOrcStatus);

            builder
                .HasOne(prop => prop.LocalEnvioNFFaturamentoVinculado)
                .WithMany()
                .HasForeignKey(prop => new { prop.CodigoPosto, prop.CodigoContrato, prop.CodLocalEnvioNFFaturamento })
                .HasPrincipalKey(prop => new { prop.CodPosto, prop.CodContrato, prop.CodLocalEnvioNFFaturamento });

        }
    }
}