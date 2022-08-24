using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresFiliaisMap : IEntityTypeConfiguration<ViewDashboardIndicadoresFiliais>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresFiliais> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_filiais")
                .HasNoKey();
        }
    }
}