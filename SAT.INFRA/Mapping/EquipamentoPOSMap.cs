using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class EquipamentoPOSMap : IEntityTypeConfiguration<EquipamentoPOS>
    {
        public void Configure(EntityTypeBuilder<EquipamentoPOS> builder)
        {
            builder.ToTable("EquipamentoPOS");
            builder.HasKey(i => i.CodEquipamentoPOS);
        }
    }
}