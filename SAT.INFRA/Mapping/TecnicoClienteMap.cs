using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TecnicoClienteMap : IEntityTypeConfiguration<TecnicoCliente>
    {
        public void Configure(EntityTypeBuilder<TecnicoCliente> builder)
        {
            builder
                .ToTable("TecnicoCliente");

            builder
                .HasKey(i => i.CodTecnicoCliente);

            builder
               .HasOne(i => i.Cliente)
               .WithMany()
               .HasForeignKey(i => i.CodCliente)
               .HasPrincipalKey(i => i.CodCliente);
        }
    }
}