using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TicketLogUsuarioCartaoPlacaMap : IEntityTypeConfiguration<TicketLogUsuarioCartaoPlaca>
    {
        public void Configure(EntityTypeBuilder<TicketLogUsuarioCartaoPlaca> builder)
        {
            builder
                .ToTable("TicketLogUsuarioCartaoPlaca");

            builder.HasKey(prop => prop
                .CodTicketLogUsuarioCartaoPlaca);
        }
    }
}