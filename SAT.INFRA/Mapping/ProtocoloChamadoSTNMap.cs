using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ProtocoloChamadoSTNMap : IEntityTypeConfiguration<ProtocoloChamadoSTN>
    {
        public void Configure(EntityTypeBuilder<ProtocoloChamadoSTN> builder)
        {
            builder
                .ToTable("ProtocoloChamadoSTN");

            builder
                .HasKey(prop => prop.CodProtocoloChamadoSTN);

            builder
                .HasOne(prop => prop.OrdemServicoSTN)
                .WithMany()
                .HasForeignKey(prop => prop.CodAtendimento)
                .HasPrincipalKey(prop => prop.CodAtendimento);

            builder
                .HasOne(prop => prop.TipoChamadoSTN)
                .WithMany()
                .HasForeignKey(prop => prop.CodTipoChamadoSTN)
                .HasPrincipalKey(prop => prop.CodTipoChamadoSTN);
        }
    }
}