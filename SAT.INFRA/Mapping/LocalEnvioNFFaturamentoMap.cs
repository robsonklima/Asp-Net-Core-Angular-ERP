using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LocalEnvioNFFaturamentoMap : IEntityTypeConfiguration<LocalEnvioNFFaturamento>
    {
        public void Configure(EntityTypeBuilder<LocalEnvioNFFaturamento> builder)
        {
            builder.ToTable("LocalEnvioNFFaturamento");
            builder.HasKey(prop => prop.CodLocalEnvioNFFaturamento);

            builder
                .HasOne(p => p.Cliente)
                .WithOne()
                .HasForeignKey<LocalEnvioNFFaturamento>("CodCliente")
                .HasPrincipalKey<Cliente>("CodCliente");

            builder
                .HasOne(p => p.Contrato)
                .WithOne()
                .HasForeignKey<LocalEnvioNFFaturamento>("CodContrato")
                .HasPrincipalKey<Contrato>("CodContrato"); 

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