using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosSPATecnicoMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosSPATecnico>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosSPATecnico> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_spa_tecnico")
                .HasNoKey();
        }
    }
}