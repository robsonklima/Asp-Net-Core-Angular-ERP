using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PlantaoTecnicoRegiaoMap : IEntityTypeConfiguration<PlantaoTecnicoRegiao>
    {
        public void Configure(EntityTypeBuilder<PlantaoTecnicoRegiao> builder)
        {
            builder.ToTable("PlantaoTecnicoRegiao");
            builder.HasKey(prop => prop.CodPlantaoTecnicoRegiao);
        }
    }
}