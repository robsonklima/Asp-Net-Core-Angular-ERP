using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LocalAtendimentoMap : IEntityTypeConfiguration<LocalAtendimento>
    {
        public void Configure(EntityTypeBuilder<LocalAtendimento> builder)
        {
            builder.
                ToTable("LocalAtendimento");

            builder.
                HasKey(i => new { i.CodPosto });

            builder
                .HasOne(prop => prop.Cliente)
                .WithMany()
                .HasForeignKey(prop => prop.CodCliente)
                .HasPrincipalKey(prop => prop.CodCliente);

            builder
                .HasOne(prop => prop.TipoRota)
                .WithMany()
                .HasForeignKey(prop => prop.CodTipoRota)
                .HasPrincipalKey(prop => prop.CodTipoRota);

            builder
                .HasOne(prop => prop.Cidade)
                .WithMany()
                .HasForeignKey(prop => prop.CodCidade)
                .HasPrincipalKey(prop => prop.CodCidade);

            builder
                .HasOne(prop => prop.Filial)
                .WithMany()
                .HasForeignKey(prop => prop.CodFilial)
                .HasPrincipalKey(prop => prop.CodFilial);

            builder
                .HasOne(prop => prop.Autorizada)
                .WithMany()
                .HasForeignKey(prop => prop.CodAutorizada)
                .HasPrincipalKey(prop => prop.CodAutorizada);
            
            builder
                .HasOne(prop => prop.Regiao)
                .WithMany()
                .HasForeignKey(prop => prop.CodRegiao)
                .HasPrincipalKey(prop => prop.CodRegiao);
        }
    }
}