using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoPecasEspecMap : IEntityTypeConfiguration<OrcamentoPecasEspec>
    {
        public void Configure(EntityTypeBuilder<OrcamentoPecasEspec> builder)
        {
            builder
                .ToTable("OrcamentoPecasEspec");

            builder
                .HasKey(i => i.CodOrcamentoPecasEspec);

            builder
                .HasOne(prop => prop.OSBancada)
                .WithMany()
                .HasForeignKey(prop => prop.CodOsbancada)
                .HasPrincipalKey(prop => prop.CodOsbancada);

            builder
                .HasOne(prop => prop.PecaRE5114)
                .WithMany()
                .HasForeignKey(prop => prop.CodPecaRe5114)
                .HasPrincipalKey(prop => prop.CodPecaRe5114);

            builder
                .HasOne(prop => prop.Peca)
                .WithMany()
                .HasForeignKey(prop => prop.CodPeca)
                .HasPrincipalKey(prop => prop.CodPeca);

            builder
                .HasOne(prop => prop.OSBancadaPecasOrcamento)
                .WithMany()
                .HasForeignKey(prop => prop.CodOrcamento)
                .HasPrincipalKey(prop => prop.CodOrcamento);

        }
    }
}