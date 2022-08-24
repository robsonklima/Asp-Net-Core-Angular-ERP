using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardReincidenciaQuadrimestreFiliaisMap : IEntityTypeConfiguration<ViewDashboardReincidenciaQuadrimestreFiliais>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardReincidenciaQuadrimestreFiliais> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_reincidencia_quadrimestre_filiais")
                .HasNoKey();
        }
    }
}