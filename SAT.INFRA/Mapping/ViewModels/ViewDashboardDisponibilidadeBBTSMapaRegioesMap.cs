using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardDisponibilidadeBBTSMapaRegioesMap : IEntityTypeConfiguration<ViewDashboardDisponibilidadeBBTSMapaRegioes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardDisponibilidadeBBTSMapaRegioes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_disponibilidade_bbts_mapa_regioes")
                .HasNoKey();
        }
    }
}