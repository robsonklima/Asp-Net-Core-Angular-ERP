using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardDisponibilidadeBBTSMultasDisponibilidadeMap : IEntityTypeConfiguration<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardDisponibilidadeBBTSMultasDisponibilidade> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_disponibilidade_bbts_multa_disponibilidade")
                .HasNoKey();
        }
    }
}