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
                .HasForeignKey<Instalacao>(i => i.CodCliente)
                .HasPrincipalKey<Cliente>(i => i.CodCliente);

            builder
                .HasOne(i => i.Filial)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodFilial)
                .HasPrincipalKey<Filial>(i => i.CodFilial);

            builder
                .HasOne(i => i.Regiao)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodRegiao)
                .HasPrincipalKey<Regiao>(i => i.CodRegiao);

            builder
                .HasOne(i => i.Autorizada)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodAutorizada)
                .HasPrincipalKey<Autorizada>(i => i.CodAutorizada);

            builder
                .HasOne(i => i.Equipamento)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodEquip)
                .HasPrincipalKey<Equipamento>(i => i.CodEquip);

            builder
               .HasOne(prop => prop.EquipamentoContrato)
               .WithMany()
               .HasForeignKey(prop => prop.CodEquipContrato)
               .HasPrincipalKey(prop => prop.CodEquipContrato);              

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
                .HasOne(i => i.Transportadora)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodTransportadora)
                .HasPrincipalKey<Transportadora>(i => i.CodTransportadora);

            builder
                .HasMany(i => i.InstalacoesRessalva)
                .WithOne()
                .HasForeignKey(i => new { i.CodInstalacao });

            builder
                .HasOne(i => i.InstalacaoNFAut)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodInstalNFAut)
                .HasPrincipalKey<InstalacaoNFAut>(i => i.CodInstalNFaut);

            builder
                .HasOne(i => i.InstalacaoNFVenda)
                .WithOne()
                .HasForeignKey<Instalacao>(i => i.CodInstalNFVenda)
                .HasPrincipalKey<InstalacaoNFVenda>(i => i.CodInstalNFvenda);
        }
    }
}
