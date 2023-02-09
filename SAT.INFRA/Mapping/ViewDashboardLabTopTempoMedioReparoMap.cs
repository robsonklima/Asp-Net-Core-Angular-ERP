using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardLabTopTempoMedioReparoMap : IEntityTypeConfiguration<ViewDashboardLabTopTempoMedioReparo>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardLabTopTempoMedioReparo> builder)
        {
            builder
                .ToView("vwc_v2_laboratorio_dashboard_top_tempo_medio_reparo")
                .HasNoKey();
        }
    }
}