using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DespesaPeriodoTecnicoMap : IEntityTypeConfiguration<DespesaPeriodoTecnico>
    {
        public void Configure(EntityTypeBuilder<DespesaPeriodoTecnico> builder)
        {
            builder
                .ToTable("DespesaPeriodoTecnico");

            builder
                .HasKey(ra => new { ra.CodTecnico, ra.CodDespesaPeriodo });

            builder
                .HasMany(i => i.Despesas)
                .WithOne()
                .HasForeignKey(i => new { i.CodTecnico, i.CodDespesaPeriodo });

            builder
                .HasOne(i => i.DespesaPeriodoTecnicoStatus)
                .WithOne()
                .HasForeignKey<DespesaPeriodoTecnico>(i => i.CodDespesaPeriodoTecnicoStatus);

            builder
                .HasOne(i => i.Tecnico)
                .WithMany()
                .HasForeignKey("CodTecnico")
                .HasPrincipalKey("CodTecnico");

            builder
                .HasOne(i => i.DespesaPeriodo)
                .WithMany()
                .HasForeignKey("CodDespesaPeriodo")
                .HasPrincipalKey("CodDespesaPeriodo");

            builder
                .HasOne(i => i.TicketLogPedidoCredito)
                .WithOne()
                .HasForeignKey<DespesaPeriodoTecnico>(i => i.CodDespesaPeriodoTecnico)
                .HasPrincipalKey<TicketLogPedidoCredito>(i => i.CodDespesaPeriodoTecnico);
        }
    }
}