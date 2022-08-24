using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosPerformanceMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosPerformance>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosPerformance> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_performance_quadrimestre")
                .HasNoKey();
        }
    }
}