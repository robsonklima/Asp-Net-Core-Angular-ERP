using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardPendenciaQuadrimestreFiliaisMap : IEntityTypeConfiguration<ViewDashboardPendenciaQuadrimestreFiliais>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardPendenciaQuadrimestreFiliais> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_pendencia_quadrimestre_filiais")
                .HasNoKey();
        }
    }
}