using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class EnderecoFaturamentoNFMap : IEntityTypeConfiguration<EnderecoFaturamentoNF>
    {
        public void Configure(EntityTypeBuilder<EnderecoFaturamentoNF> builder)
        {
            builder
                .ToTable("LocalEnvioNFFaturamento");

            builder
                .HasKey(prop => prop.CodLocalEnvioNFFaturamento);

            builder
                .HasOne(prop => prop.CidadeEnvioNF)
                .WithMany()
                .HasForeignKey(prop => prop.CodCidadeEnvioNF)
                .HasPrincipalKey(prop => prop.CodCidade);

            builder
               .HasOne(prop => prop.CidadeFaturamento)
               .WithMany()
               .HasForeignKey(prop => prop.CodCidadeFaturamento)
               .HasPrincipalKey(prop => prop.CodCidade);
        }
    }
}