using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardPendenciaFiliaisMap : IEntityTypeConfiguration<ViewDashboardPendenciaFiliais>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardPendenciaFiliais> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_pendencia_filiais")
                .HasNoKey();
        }
    }
}