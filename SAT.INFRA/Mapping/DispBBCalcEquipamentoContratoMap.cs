using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DispBBCalcEquipamentoContratoMap : IEntityTypeConfiguration<DispBBCalcEquipamentoContrato>
    {
        public void Configure(EntityTypeBuilder<DispBBCalcEquipamentoContrato> builder)
        {
            builder
                .ToTable("DispBBCalcEquipamentoContrato");

            builder
                .HasNoKey();

        }
    }
}