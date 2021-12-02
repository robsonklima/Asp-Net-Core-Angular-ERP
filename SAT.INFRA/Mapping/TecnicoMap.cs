using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TecnicoMap : IEntityTypeConfiguration<Tecnico>
    {
        public void Configure(EntityTypeBuilder<Tecnico> builder)
        {
            builder
                .ToTable("Tecnico");

            builder
                .HasKey(prop => prop.CodTecnico);

            builder
                .HasMany<OrdemServico>(os => os.OrdensServico);

            builder
                .HasMany<DespesaCartaoCombustivelTecnico>(e => e.DespesaCartaoCombustivelTecnico)
                .WithOne()
                .HasForeignKey(i => i.CodTecnico)
                .HasPrincipalKey(i => i.CodTecnico);

            builder
                .HasOne(i => i.Autorizada)
                .WithMany()
                .HasForeignKey("CodAutorizada")
                .HasPrincipalKey("CodAutorizada");

            builder
                .HasOne(i => i.RegiaoAutorizada)
                .WithMany()
                .HasForeignKey(i => new { i.CodFilial, i.CodRegiao, i.CodAutorizada });

            builder
                .HasOne(i => i.Regiao)
                .WithMany()
                .HasForeignKey("CodRegiao")
                .HasPrincipalKey("CodRegiao");

            builder
                .HasOne(i => i.Cidade)
                .WithMany()
                .HasForeignKey("CodCidade")
                .HasPrincipalKey("CodCidade");

            builder
                .HasOne(i => i.TipoRota)
                .WithMany()
                .HasForeignKey("CodTipoRota")
                .HasPrincipalKey("CodTipoRota");

            builder
                .HasOne(i => i.Filial)
                .WithMany()
                .HasForeignKey("CodFilial")
                .HasPrincipalKey("CodFilial");

            builder
                .HasMany(i => i.DespesaCartaoCombustivelTecnico)
                .WithOne()
                .HasForeignKey(i => i.CodTecnico)
                .HasPrincipalKey(i => i.CodTecnico);

            builder
                .HasMany(i => i.TecnicoConta)
                .WithOne()
                .HasForeignKey("CodTecnico")
                .HasPrincipalKey("CodTecnico");

            builder
                .Ignore(i => i.MediaTempoAtendMin)
                .Ignore(i => i.TecnicoCategoriaCredito);
        }
    }
}