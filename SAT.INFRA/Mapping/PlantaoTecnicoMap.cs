using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PlantaoTecnicoMap : IEntityTypeConfiguration<PlantaoTecnico>
    {
        public void Configure(EntityTypeBuilder<PlantaoTecnico> builder)
        {
            builder.ToTable("PlantaoTecnico");
            builder.HasKey(prop => prop.CodPlantaoTecnico);

            builder
                .HasOne(p => p.Tecnico)
                .WithMany()
                .HasForeignKey("CodTecnico")
                .HasPrincipalKey("CodTecnico");
        }
    }
}