using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardReincidenciaFiliaisMap : IEntityTypeConfiguration<ViewDashboardReincidenciaFiliais>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardReincidenciaFiliais> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_reincidencia_filiais")
                .HasNoKey();
        }
    }
}