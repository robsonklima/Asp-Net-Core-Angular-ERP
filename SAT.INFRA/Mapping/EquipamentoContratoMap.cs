using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class EquipamentoContratoMap : IEntityTypeConfiguration<EquipamentoContrato>
    {
        public void Configure(EntityTypeBuilder<EquipamentoContrato> builder)
        {
            builder
                .ToTable("EquipamentoContrato");

            builder
                .HasKey(prop => prop.CodEquipContrato);

            builder
                .HasOne(prop => prop.Contrato)
                .WithMany()
                .HasForeignKey(prop => prop.CodContrato)
                .HasPrincipalKey(prop => prop.CodContrato);

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

            builder
                .HasOne(prop => prop.AcordoNivelServico)
                .WithMany()
                .HasForeignKey(prop => prop.CodSLA)
                .HasPrincipalKey(prop => prop.CodSLA);

            builder
                .HasOne(prop => prop.Cliente)
                .WithMany()
                .HasForeignKey(prop => prop.CodCliente)
                .HasPrincipalKey(prop => prop.CodCliente);

            builder
                .HasOne(prop => prop.LocalAtendimento)
                .WithMany()
                .HasForeignKey(prop => prop.CodPosto)
                .HasPrincipalKey(prop => prop.CodPosto);

            builder
                .HasOne(prop => prop.Regiao)
                .WithMany()
                .HasForeignKey(prop => prop.CodRegiao)
                .HasPrincipalKey(prop => prop.CodRegiao);

            builder
                .HasOne(prop => prop.Autorizada)
                .WithMany()
                .HasForeignKey(prop => prop.CodAutorizada)
                .HasPrincipalKey(prop => prop.CodAutorizada);

            builder
                .HasOne(prop => prop.Filial)
                .WithMany()
                .HasForeignKey(prop => prop.CodFilial)
                .HasPrincipalKey(prop => prop.CodFilial);

            builder
                .HasOne(prop => prop.DispBBCriticidade)
                .WithMany()
                .HasForeignKey(prop => prop.CodSLA)
                .HasPrincipalKey(prop => prop.CodSLA);

            builder
                .HasOne(prop => prop.RegiaoAutorizada)
                .WithMany()
                .HasForeignKey(prop => new { prop.CodFilial, prop.CodRegiao, prop.CodAutorizada})
                .HasPrincipalKey((prop => new { prop.CodFilial, prop.CodRegiao, prop.CodAutorizada }));
        }
    }
}