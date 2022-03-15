using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosSLATecnicoMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosSLATecnico>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosSLATecnico> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_sla_tecnico")
                .HasNoKey();
        }
    }
}