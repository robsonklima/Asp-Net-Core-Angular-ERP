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
            builder.HasKey(prop => new { prop.CodContrato, prop.CodGrupoEquip, prop.CodTipoEquip, prop.CodEquip});

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

            builder
                .HasOne(prop => prop.Equipamento)
                .WithMany()
                .HasForeignKey(prop => prop.CodEquip)
                .HasPrincipalKey(prop => prop.CodEquip);
        }
    }
}
