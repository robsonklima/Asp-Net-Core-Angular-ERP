using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardLabTopFaltantesMap : IEntityTypeConfiguration<ViewDashboardLabTopFaltantes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardLabTopFaltantes> builder)
        {
            builder
                .ToView("vwc_v2_laboratorio_dashboard_top_faltantes")
                .HasNoKey();
        }
    }
}