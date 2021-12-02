using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TicketLogPedidoCreditoMap : IEntityTypeConfiguration<TicketLogPedidoCredito>
    {
        public void Configure(EntityTypeBuilder<TicketLogPedidoCredito> builder)
        {
            builder
                .ToTable("TicketLogPedidoCredito");

            builder.HasKey(prop => prop
                .CodTicketLogPedidoCredito);
        }
    }
}