using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ConferenciaMap : IEntityTypeConfiguration<Conferencia>
    {
        public void Configure(EntityTypeBuilder<Conferencia> builder)
        {
            builder.ToTable("Conferencia");

            builder.HasKey(i => i.CodConferencia);

            builder
                .HasOne(prop => prop.UsuarioCadastro)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioCad)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.UsuarioManut)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioManut)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasMany(prop => prop.Participantes)
                .WithOne()
                .HasForeignKey(prop => prop.CodConferencia)
                .HasPrincipalKey(prop => prop.CodConferencia);
        }
    }
}