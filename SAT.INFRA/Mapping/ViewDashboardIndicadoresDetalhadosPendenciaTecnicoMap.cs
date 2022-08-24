using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosPendenciaTecnicoMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosPendenciaTecnico>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosPendenciaTecnico> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_pendencia_tecnico")
                .HasNoKey();
        }
    }
}