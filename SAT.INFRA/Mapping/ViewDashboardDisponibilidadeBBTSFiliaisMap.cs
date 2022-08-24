using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardDisponibilidadeBBTSFiliaisMap : IEntityTypeConfiguration<ViewDashboardDisponibilidadeBBTSFiliais>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardDisponibilidadeBBTSFiliais> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_disponibilidade_bbts_filiais")
                .HasNoKey();
        }
    }
}