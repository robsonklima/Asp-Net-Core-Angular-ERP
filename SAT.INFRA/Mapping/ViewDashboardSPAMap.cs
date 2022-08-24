using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardSPAMap : IEntityTypeConfiguration<ViewDashboardSPA>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardSPA> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_SPA")
                .HasNoKey();
        }
    }
}