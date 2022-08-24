using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardDisponibilidadeTecnicosMediaGlobalMap : IEntityTypeConfiguration<ViewDashboardDisponibilidadeTecnicosMediaGlobal>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardDisponibilidadeTecnicosMediaGlobal> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_disponibilidade_tecnicos_media_global")
                .HasNoKey();
        }
    }
}