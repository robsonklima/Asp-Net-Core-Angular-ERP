using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class UnidadeFederativaMap : IEntityTypeConfiguration<UnidadeFederativa>
    {
        public void Configure(EntityTypeBuilder<UnidadeFederativa> builder)
        {
            builder
                .ToTable("UF");

            builder
                .HasKey(prop => prop.CodUF);

            builder
                .HasOne(prop => prop.Pais)
                .WithMany()
                .HasForeignKey(prop => prop.CodPais)
                .HasPrincipalKey(prop => prop.CodPais);

            builder
                .HasOne(prop => prop.DispBBRegiaoUF)
                .WithMany()
                .HasForeignKey(prop => prop.CodUF)
                .HasPrincipalKey(prop => prop.CodUF);
        }
    }
}