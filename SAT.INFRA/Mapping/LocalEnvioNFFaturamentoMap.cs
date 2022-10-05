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
                .HasOne(prop => prop.Cliente)
                .WithMany()
                .HasForeignKey(prop => prop.CodCliente)
                .HasPrincipalKey(prop => prop.CodCliente);

            builder
                .HasOne(prop => prop.Contrato)
                .WithMany()
                .HasForeignKey(prop => prop.CodContrato)
                .HasPrincipalKey(prop => prop.CodContrato);

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