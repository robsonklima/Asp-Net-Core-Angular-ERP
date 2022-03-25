using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class EquipamentoMap : IEntityTypeConfiguration<Equipamento>
    {
        public void Configure(EntityTypeBuilder<Equipamento> builder)
        {
            builder
                .ToTable("Equipamento");

            builder
                .HasKey(prop => prop.CodEquip);

            builder
                .HasOne(prop => prop.TipoEquipamento)
                .WithMany()
                .HasForeignKey(prop => prop.CodTipoEquip)
                .HasPrincipalKey(prop => prop.CodTipoEquip);

            builder
            .HasOne(prop => prop.GrupoEquipamento)
            .WithMany()
            .HasForeignKey(prop => prop.CodGrupoEquip)
            .HasPrincipalKey(prop => prop.CodGrupoEquip);
        }
    }
}