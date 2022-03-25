using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TipoEquipamentoMap : IEntityTypeConfiguration<TipoEquipamento>
    {
        public void Configure(EntityTypeBuilder<TipoEquipamento> builder)
        {
            builder
                .ToTable("TipoEquipamento");

            builder
                .HasKey(prop => prop.CodTipoEquip);
        }
    }
}