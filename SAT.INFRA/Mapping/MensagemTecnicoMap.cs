using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MensagemTecnicoMap : IEntityTypeConfiguration<MensagemTecnico>
    {
        public void Configure(EntityTypeBuilder<MensagemTecnico> builder)
        {
            builder.ToTable("MensagemTecnico");
            builder.HasKey(i => i.CodMensagemTecnico);

            builder
                .HasOne(i => i.UsuarioDestinatario)
                .WithMany()
                .HasForeignKey(i => i.CodUsuarioDestinatario)
                .HasPrincipalKey(i => i.CodUsuario);
                
            builder
                .HasOne(i => i.UsuarioCad)
                .WithMany()
                .HasForeignKey(i => i.CodUsuarioCad)
                .HasPrincipalKey(i => i.CodUsuario); 
        }
    }
}