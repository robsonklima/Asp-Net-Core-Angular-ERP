using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class StatusEquipamentoPOSMap : IEntityTypeConfiguration<StatusEquipamentoPOS>
    {
        public void Configure(EntityTypeBuilder<StatusEquipamentoPOS> builder)
        {
            builder
                .ToTable("StatusEquipamentoPOS");

            builder
                .HasKey(i => i.CodStatusEquipamentoPos);
        }
    }
}