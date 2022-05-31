using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoMap : IEntityTypeConfiguration<Instalacao>
    {
        public void Configure(EntityTypeBuilder<Instalacao> builder)
        {
            builder.ToTable("Instalacao");

            builder
                .HasKey(i => new { i.CodInstalacao });

            builder
                .HasOne(i => i.Cliente)
                .WithOne()
                .HasForeignKey<Cliente>(i => i.CodCliente)
                .HasPrincipalKey<Instalacao>(i => i.CodCliente);

            builder
                .HasOne(i => i.Filial)
                .WithOne()
                .HasForeignKey<Filial>(i => i.CodFilial)
                .HasPrincipalKey<Instalacao>(i => i.CodFilial);

            builder
                .HasOne(i => i.Equipamento)
                .WithOne()
                .HasForeignKey<Equipamento>(i => i.CodEquip)
                .HasPrincipalKey<Instalacao>(i => i.CodEquip);

            // builder
            //     .HasOne(i => i.EquipamentoContrato)
            //     .WithOne()
            //     .HasForeignKey<EquipamentoContrato>(i => i.CodEquipContrato)
            //     .HasPrincipalKey<Instalacao>(i => i.CodEquipContrato);

            builder
                .HasOne(i => i.InstalacaoLote)
                .WithOne()
                .HasForeignKey<InstalacaoLote>(i => i.CodInstalLote)
                .HasPrincipalKey<Instalacao>(i => i.CodInstalLote);

            builder
                .HasOne(i => i.Contrato)
                .WithOne()
                .HasForeignKey<Contrato>(i => i.CodContrato)
                .HasPrincipalKey<Instalacao>(i => i.CodContrato);

            builder
                .HasOne(i => i.LocalAtendimentoIns)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodPostoIns)
                .HasPrincipalKey<LocalAtendimento>(i => i.CodPosto);

            builder
                .HasOne(i => i.LocalAtendimentoSol)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodPosto)
                .HasPrincipalKey<LocalAtendimento>(i => i.CodPosto);

            builder
                .HasOne(i => i.LocalAtendimentoEnt)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodPostoEnt)
                .HasPrincipalKey<LocalAtendimento>(i => i.CodPosto);

            builder
                .HasOne(i => i.OrdemServico)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodOS)
                .HasPrincipalKey<OrdemServico>(i => i.CodOS);                

            builder
                .HasOne(i => i.InstalacaoStatus)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodInstalStatus)
                .HasPrincipalKey<InstalacaoStatus>(i => i.CodInstalStatus);  

            builder
                .HasMany(i => i.InstalacoesRessalva)
                .WithOne()
                .HasForeignKey(i => new { i.CodInstalacao });    
        }
    }
}
