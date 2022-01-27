using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class VersaoAlteracaoMap : IEntityTypeConfiguration<VersaoAlteracao>
    {
        public void Configure(EntityTypeBuilder<VersaoAlteracao> builder)
        {
            builder.ToTable("SatVersaoAlteracao");
            builder.HasKey(prop => prop.CodSatVersaoAlteracao);
            builder
                .HasOne(prop => prop.Tipo)
                .WithMany()
                .HasForeignKey(prop => prop.CodSatVersaoAlteracaoTipo)
                .HasPrincipalKey(prop => prop.CodSatVersaoAlteracaoTipo);
        }
    }
}