using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TicketLogTransacaoMap : IEntityTypeConfiguration<TicketLogTransacao>
    {
        public void Configure(EntityTypeBuilder<TicketLogTransacao> builder)
        {
            builder.ToTable("TicketLogTransacao");
            builder.HasKey(prop => prop.CodTicketLogTransacao);
        }
    }
}