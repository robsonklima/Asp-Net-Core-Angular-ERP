using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PontoUsuarioMap : IEntityTypeConfiguration<PontoUsuario>
    {
        public void Configure(EntityTypeBuilder<PontoUsuario> builder)
        {
            builder.ToTable("PontoUsuario");
            builder.HasKey(prop => prop.CodPontoUsuario);

            builder
                .HasOne(prop => prop.PontoPeriodo)
                .WithMany()
                .HasForeignKey(prop => prop.CodPontoPeriodo)
                .HasPrincipalKey(prop => prop.CodPontoPeriodo);
        }
    }
}

