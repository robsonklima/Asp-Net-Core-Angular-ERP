using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TicketAtendimentoMap : IEntityTypeConfiguration<TicketAtendimento>
    {
        public void Configure(EntityTypeBuilder<TicketAtendimento> builder)
        {
            builder.ToTable("TicketAtendimento");
            builder.HasKey(prop => prop.CodTicketAtend);

            builder
                 .HasOne(prop => prop.TicketStatus)
                 .WithMany()
                 .HasForeignKey(prop => prop.CodStatus)
                 .HasPrincipalKey(prop => prop.CodStatus);

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