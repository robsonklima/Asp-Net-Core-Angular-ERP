using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping {
    public class TicketMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.HasKey(i => new { i.CodTicket });

            builder
                .HasOne(prop => prop.TicketModulo)
                .WithMany()
                .HasForeignKey(prop => prop.CodModulo)
                .HasPrincipalKey(prop => prop.CodModulo);

            builder
                .HasOne(prop => prop.TicketClassificacao)
                .WithMany()
                .HasForeignKey(prop => prop.CodClassificacao)
                .HasPrincipalKey(prop => prop.CodClassificacao);

            builder
                .HasOne(prop => prop.TicketPrioridade)
                .WithMany()
                .HasForeignKey(prop => prop.CodPrioridade)
                .HasPrincipalKey(prop => prop.CodPrioridade);
                
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

            builder
                .HasOne(prop => prop.UsuarioAtendente)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioAtendente)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasMany(prop => prop.Atendimentos)
                .WithOne()
                .HasForeignKey(prop => prop.CodTicket)
                .HasPrincipalKey(prop => prop.CodTicket);

            builder
                .HasMany(prop => prop.Anexos)
                .WithOne()
                .HasForeignKey(prop => prop.CodTicket)
                .HasPrincipalKey(prop => prop.CodTicket);
        }
    }
}