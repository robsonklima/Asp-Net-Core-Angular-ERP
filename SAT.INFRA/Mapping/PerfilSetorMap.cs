using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PerfilSetorMap : IEntityTypeConfiguration<PerfilSetor>
    {
        public void Configure(EntityTypeBuilder<PerfilSetor> builder)
        {
            builder.ToTable("PerfilSetor");
            builder.HasKey(prop => prop.CodPerfilSetor);

            builder
                .HasOne(prop => prop.Setor)
                .WithMany()
                .HasForeignKey(prop => prop.CodSetor)
                .HasPrincipalKey(prop => prop.CodSetor);

            builder
                .HasOne(prop => prop.Perfil)
                .WithMany()
                .HasForeignKey(prop => prop.CodPerfil)
                .HasPrincipalKey(prop => prop.CodPerfil);
        }
    }
}