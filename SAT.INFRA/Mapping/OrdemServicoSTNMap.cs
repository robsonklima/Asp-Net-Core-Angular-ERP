using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrdemServicoSTNMap : IEntityTypeConfiguration<OrdemServicoSTN>
    {
        public void Configure(EntityTypeBuilder<OrdemServicoSTN> builder)
        {
            builder.ToTable("ChamadosSTN");
            builder.HasKey(i => i.CodAtendimento);

            builder
                .Property(e => e.CodTecnico)
                .HasConversion<string>();

            builder
                .HasOne(prop => prop.OrdemServico)
                .WithMany()
                .HasForeignKey(prop => prop.CodOS)
                .HasPrincipalKey(prop => prop.CodOS);

            builder
                .HasOne(prop => prop.StatusSTN)
                .WithMany()
                .HasForeignKey(prop => prop.CodStatusSTN)
                .HasPrincipalKey(prop => prop.CodStatusServicoSTN);

            builder
                .HasOne(prop => prop.OrdemServicoSTNOrigem)
                .WithMany()
                .HasForeignKey(prop => prop.CodOrigemChamadoSTN)
                .HasPrincipalKey(prop => prop.CodOrigemChamadoSTN);

            builder
                .HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop =>  prop.CodTecnico)
                .HasPrincipalKey(prop => prop.CodUsuario);
        }
    }
}