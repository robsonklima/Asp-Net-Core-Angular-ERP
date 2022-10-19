using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MensagemMap : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            builder.ToTable("Msg");
            builder.HasKey(i => i.CodMsg);

            builder
                .HasOne(prop => prop.UsuarioRemetente)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioRemetente)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.UsuarioDestinatario)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioDestinatario)
                .HasPrincipalKey(prop => prop.CodUsuario);
        }
    }
}