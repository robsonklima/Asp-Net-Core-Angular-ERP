using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndiceReincidenciaMap : IEntityTypeConfiguration<ViewDashboardLabTopItensMaisAntigos>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardLabTopItensMaisAntigos> builder)
        {
            builder
                .ToView("vwc_v2_laboratorio_dashboard_indice_reincidencia")
                .HasNoKey();
        }
    }
}