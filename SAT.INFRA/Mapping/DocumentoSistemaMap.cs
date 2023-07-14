using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DocumentoSistemaMap : IEntityTypeConfiguration<DocumentoSistema>
    {
        public void Configure(EntityTypeBuilder<DocumentoSistema> builder)
        {
            builder.ToTable("DocumentoSistema");
            builder.HasKey(prop => prop.CodDocumentoSistema);

            builder
                .HasOne(prop => prop.UsuarioCad)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioCad)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.UsuarioManut)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioManut)
                .HasPrincipalKey(prop => prop.CodUsuario);
        }
    }
}