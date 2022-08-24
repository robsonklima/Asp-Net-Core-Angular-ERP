using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardPendenciaGlobalMap : IEntityTypeConfiguration<ViewDashboardPendenciaGlobal>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardPendenciaGlobal> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_pendencia_filiais_pendencia_global")
                .HasNoKey();
        }
    }
}