using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class CausaImprodutividadeMap : IEntityTypeConfiguration<CausaImprodutividade>
    {
        public void Configure(EntityTypeBuilder<CausaImprodutividade> builder)
        {
            builder
                .ToTable("CausaImprodutividade");

            builder
                .HasKey(prop => prop.CodCausaImprodutividade);

            builder
                .HasOne(prop => prop.ProtocoloChamadoSTN)
                .WithMany()
                .HasForeignKey(prop => prop.CodProtocolo)
                .HasPrincipalKey(prop => prop.CodProtocoloChamadoSTN);

            builder
                .HasOne(prop => prop.Improdutividade)
                .WithMany()
                .HasForeignKey(prop => prop.CodImprodutividade)
                .HasPrincipalKey(prop => prop.CodImprodutividade);
        }
    }
}