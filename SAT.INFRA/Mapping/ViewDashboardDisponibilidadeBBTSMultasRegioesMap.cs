using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardDisponibilidadeBBTSMultasRegioesMap : IEntityTypeConfiguration<ViewDashboardDisponibilidadeBBTSMultasRegioes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardDisponibilidadeBBTSMultasRegioes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_disponibilidade_bbts_valores_multas_regioes")
                .HasNoKey();
        }
    }
}