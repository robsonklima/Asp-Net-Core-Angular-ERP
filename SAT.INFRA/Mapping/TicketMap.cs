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
                .HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuario)
                .HasPrincipalKey(prop => prop.CodUsuario);

        }
    }
}