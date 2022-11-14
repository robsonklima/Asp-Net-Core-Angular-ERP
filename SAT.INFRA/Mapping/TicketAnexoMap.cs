using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TicketAnexoMap : IEntityTypeConfiguration<TicketAnexo>
    {
        public void Configure(EntityTypeBuilder<TicketAnexo> builder)
        {
            builder.ToTable("TicketAnexo");
            builder.HasKey(prop => prop.CodTicketAnexo);

            builder
                .HasOne(i => i.UsuarioCad)
                .WithMany()
                .HasForeignKey(p => p.CodUsuarioCad)
                .HasPrincipalKey(p => p.CodUsuario);
        }
    }
}