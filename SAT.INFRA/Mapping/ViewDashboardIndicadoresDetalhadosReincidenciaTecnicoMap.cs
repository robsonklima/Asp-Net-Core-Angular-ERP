using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosReincidenciaTecnicoMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosReincidenciaTecnico>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosReincidenciaTecnico> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_reincidencia_tecnico")
                .HasNoKey();
        }
    }
}