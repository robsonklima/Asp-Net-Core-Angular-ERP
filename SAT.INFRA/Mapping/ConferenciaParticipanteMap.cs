using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ConferenciaParticipanteMap : IEntityTypeConfiguration<ConferenciaParticipante>
    {
        public void Configure(EntityTypeBuilder<ConferenciaParticipante> builder)
        {
            builder.ToTable("ConferenciaParticipante");
            builder.HasKey(prop => prop.CodConferenciaParticipante);

            builder
                .HasOne(p => p.UsuarioCadastro)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioCad)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(p => p.UsuarioParticipante)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioParticipante)
                .HasPrincipalKey(prop => prop.CodUsuario);
        }
    }
}