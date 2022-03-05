using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PlantaoTecnicoClienteMap : IEntityTypeConfiguration<PlantaoTecnicoCliente>
    {
        public void Configure(EntityTypeBuilder<PlantaoTecnicoCliente> builder)
        {
            builder.ToTable("PlantaoTecnicoCliente");
            builder.HasKey(prop => prop.CodPlantaoTecnicoCliente);

            builder
                .HasOne(prop => prop.Cliente)
                .WithMany()
                .HasForeignKey(prop => prop.CodCliente)
                .HasPrincipalKey(prop => prop.CodCliente);
        }
    }
}