using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class EquipamentoModuloMap : IEntityTypeConfiguration<EquipamentoModulo>
    {
        public void Configure(EntityTypeBuilder<EquipamentoModulo> builder)
        {
            builder
                .ToTable("ConfigEquipModulos");

            builder
                .HasKey(h => h.CodConfigEquipModulos);

            builder
                .HasOne(i => i.Causa)
                .WithMany()
                .HasForeignKey("CodECausa")
                .HasPrincipalKey("CodECausa");

            builder
                .HasOne(i => i.Equipamento)
                .WithMany()
                .HasForeignKey("CodEquip")
                .HasPrincipalKey("CodEquip");
        }
    }
}