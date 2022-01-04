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
                .HasOne(prop => prop.EnderecoFaturamentoNF)
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
                .WithOne()
                .HasForeignKey<OrcamentoMotivo>(i => i.CodOrcMotivo)
                .HasPrincipalKey<Orcamento>(i => i.CodigoMotivo);

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
               .HasMany(i => i.OutrosServicos)
               .WithOne()
               .HasForeignKey(i => i.CodOrc)
               .HasPrincipalKey(i => i.CodOrc);

            builder
               .HasMany(i => i.Descontos)
               .WithOne()
               .HasForeignKey(i => i.CodOrc)
               .HasPrincipalKey(i => i.CodOrc);
        }
    }
}