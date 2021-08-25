using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ContratoEquipamentoMap : IEntityTypeConfiguration<ContratoEquipamento>
    {
        public void Configure(EntityTypeBuilder<ContratoEquipamento> builder)
        {
            builder.ToTable("ContratoEquipamento");

            builder.HasKey(prop => prop.CodContrato);
            builder.HasKey(prop => prop.CodGrupoEquip);
            builder.HasKey(prop => prop.CodTipoEquip);
            builder.HasKey(prop => prop.CodEquip);
        }
    }
}
