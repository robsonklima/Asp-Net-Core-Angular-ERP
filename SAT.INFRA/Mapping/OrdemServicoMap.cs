using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrdemServicoMap : IEntityTypeConfiguration<OrdemServico>
    {
        public void Configure(EntityTypeBuilder<OrdemServico> builder)
        {
            builder
                .ToTable("OS");

            builder
                .HasKey(prop => prop.CodOS);

            builder
                .Ignore(prop => prop.IndNumRATObrigatorio)
                .Ignore(prop => prop.Fotos)
                .Ignore(prop => prop.Alertas);

            builder
                .HasOne(prop => prop.StatusServico)
                .WithMany()
                .HasForeignKey(prop => prop.CodStatusServico)
                .HasPrincipalKey(prop => prop.CodStatusServico);

            builder
                .HasOne(prop => prop.TipoIntervencao)
                .WithMany()
                .HasForeignKey(prop => prop.CodTipoIntervencao)
                .HasPrincipalKey(prop => prop.CodTipoIntervencao);

            builder
                .HasOne(prop => prop.LocalAtendimento)
                .WithMany()
                .HasForeignKey(prop => prop.CodPosto)
                .HasPrincipalKey(prop => prop.CodPosto);

            builder
               .HasOne(prop => prop.EquipamentoContrato)
               .WithMany()
               .HasForeignKey(prop => prop.CodEquipContrato)
               .HasPrincipalKey(prop => prop.CodEquipContrato);

            builder
                .HasOne(prop => prop.Equipamento)
                .WithMany()
                .HasForeignKey(prop => prop.CodEquip)
                .HasPrincipalKey(prop => prop.CodEquip);

            builder
                .HasMany(prop => prop.RelatoriosAtendimento)
                .WithOne()
                .HasForeignKey(prop => prop.CodOS)
                .HasPrincipalKey(prop => prop.CodOS);

            builder
               .HasOne(prop => prop.Cliente)
               .WithMany()
               .HasForeignKey(prop => prop.CodCliente)
               .HasPrincipalKey(prop => prop.CodCliente);

            builder
                .HasOne(prop => prop.Tecnico)
                .WithMany(prop => prop.OrdensServico)
                .HasForeignKey(prop => prop.CodTecnico)
                .HasPrincipalKey(prop => prop.CodTecnico);

            builder
                .HasOne(prop => prop.Filial)
                .WithMany(prop => prop.OrdensServico)
                .HasForeignKey(prop => prop.CodFilial)
                .HasPrincipalKey(prop => prop.CodFilial);

            builder
               .HasOne(prop => prop.Autorizada)
               .WithMany()
               .HasForeignKey(prop => prop.CodAutorizada)
               .HasPrincipalKey(prop => prop.CodAutorizada);

            builder
                .HasOne(prop => prop.Regiao)
                .WithMany()
                .HasForeignKey(prop => prop.CodRegiao)
                .HasPrincipalKey(prop => prop.CodRegiao);

            builder
                .HasOne(prop => prop.RegiaoAutorizada)
                .WithMany()
                .HasForeignKey(prop => new { prop.CodFilial, prop.CodRegiao, prop.CodAutorizada })
                .HasPrincipalKey(prop => new { prop.CodFilial, prop.CodRegiao, prop.CodAutorizada });

            builder
                .HasMany(prop => prop.PrazosAtendimento)
                .WithOne()
                .HasForeignKey(prop => prop.CodOS)
                .HasPrincipalKey(prop => prop.CodOS);

            builder
               .HasMany(prop => prop.OrdensServicoRelatorioInstalacao)
               .WithOne()
               .HasForeignKey(prop => prop.CodOS)
               .HasPrincipalKey(prop => prop.CodOS);

            builder
               .HasOne(prop => prop.DispBBEquipamentoContrato)
               .WithMany()
               .HasForeignKey(prop => prop.CodEquipContrato)
               .HasPrincipalKey(prop => prop.CodEquipContrato);

            builder
                .HasOne(prop => prop.Contrato)
                .WithMany()
                .HasForeignKey(prop => prop.CodContrato)
                .HasPrincipalKey(prop => prop.CodContrato);

            builder
               .HasMany(prop => prop.Agendamentos)
               .WithOne()
               .HasForeignKey(prop => prop.CodOS)
               .HasPrincipalKey(prop => prop.CodOS);

            builder
               .HasMany(prop => prop.Intencoes)
               .WithOne()
               .HasForeignKey(prop => prop.CodOS)
               .HasPrincipalKey(prop => prop.CodOS);
        }
    }
}