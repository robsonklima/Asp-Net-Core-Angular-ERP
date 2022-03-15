using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosChamadosAntigosMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosChamadosAntigos>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosChamadosAntigos> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_chamados_antigos")
                .HasNoKey();
        }
    }
}