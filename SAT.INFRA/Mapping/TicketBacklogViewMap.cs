using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TicketBacklogViewMap : IEntityTypeConfiguration<TicketBacklogView>
    {
        public void Configure(EntityTypeBuilder<TicketBacklogView> builder)
        {
            builder.ToTable("vwc_v2_ticketbacklog");
            builder.HasNoKey();
        }
    }
}