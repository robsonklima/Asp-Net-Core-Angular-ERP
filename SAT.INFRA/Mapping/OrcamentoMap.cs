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
                .HasOne(prop => prop.LocalEnvioNFFaturamento)
                .WithMany()
                .HasForeignKey(prop => new { prop.CodigoCliente, prop.CodigoContrato })
                .HasPrincipalKey(prop => new { prop.CodCliente, prop.CodContrato });

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
               .HasMany(i => i.Materiais)
               .WithOne()
               .HasForeignKey(i => i.CodOrc)
               .HasPrincipalKey(i => i.CodOrc);

            builder
                .HasOne(i => i.MaoDeObra)
                .WithOne()
                .HasForeignKey<OrcamentoMaoDeObra>(i => i.CodOrc)
                .HasPrincipalKey<Orcamento>(i => i.CodOrc);

            builder
                .HasOne(i => i.OrcamentoDeslocamento)
                .WithOne()
                .HasForeignKey<OrcamentoDeslocamento>(i => i.CodOrc)
                .HasPrincipalKey<Orcamento>(i => i.CodOrc);

            builder
               .HasMany(i => i.OutrosServicos)
               .WithOne()
               .HasForeignKey(i => i.CodOrc)
               .HasPrincipalKey(i => i.CodOrc);

            builder
               .HasMany(i => i.Descontos)
               .WithOne()
               .HasForeignKey(i => i.CodOrc)
               .HasPrincipalKey(i => i.CodOrc);

            builder
                .HasOne(i => i.OrcamentoStatus)
                .WithMany()
                .HasForeignKey(i => i.CodigoStatus)
                .HasPrincipalKey(i => i.CodOrcStatus);
        }
    }
}