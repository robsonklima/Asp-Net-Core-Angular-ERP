using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORItemMap : IEntityTypeConfiguration<ORItem>
    {
        public void Configure(EntityTypeBuilder<ORItem> builder)
        {
            builder.ToTable("ORItem");
            builder.HasKey(i => i.CodORItem);

            builder
                .HasOne(prop => prop.Peca)
                .WithMany()
                .HasForeignKey(prop => prop.CodPeca)
                .HasPrincipalKey(prop => prop.CodPeca);

            builder
                .HasOne(prop => prop.ORTipo)
                .WithMany()
                .HasForeignKey(prop => prop.CodTipoOR)
                .HasPrincipalKey(prop => prop.CodTipoOR);

            builder
                .HasOne(prop => prop.OrdemServico)
                .WithMany()
                .HasForeignKey(prop => prop.CodOS)
                .HasPrincipalKey(prop => prop.CodOS);

            builder
                .HasOne(prop => prop.Cliente)
                .WithMany()
                .HasForeignKey(prop => prop.CodCliente)
                .HasPrincipalKey(prop => prop.CodCliente);

            builder
                .HasOne(prop => prop.UsuarioTecnico)
                .WithMany()
                .HasForeignKey(prop => prop.CodTecnico)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.UsuarioCadastro)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioCad)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.StatusOR)
                .WithMany()
                .HasForeignKey(prop => prop.CodStatus)
                .HasPrincipalKey(prop => prop.CodStatus);

            builder
                .HasMany(prop => prop.TemposReparo)
                .WithOne()
                .HasForeignKey(prop => prop.CodORItem)
                .HasPrincipalKey(prop => prop.CodORItem);

            builder
                .HasOne(prop => prop.ORDefeito)
                .WithMany()
                .HasForeignKey(prop => prop.CodDefeito)
                .HasPrincipalKey(prop => prop.CodDefeito);
            
            builder
                .HasOne(prop => prop.ORSolucao)
                .WithMany()
                .HasForeignKey(prop => prop.CodSolucao)
                .HasPrincipalKey(prop => prop.CodSolucao);

            builder.Ignore(p => p.DiasEmReparo);
        }
    }
}