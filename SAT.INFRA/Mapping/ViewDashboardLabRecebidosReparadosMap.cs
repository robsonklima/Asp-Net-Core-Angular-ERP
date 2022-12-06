using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardLabRecebidosReparadosMap : IEntityTypeConfiguration<ViewDashboardLabRecebidosReparados>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardLabRecebidosReparados> builder)
        {
            builder
                .ToView("vwc_v2_laboratorio_dashboard_reparos_recebidos_sucatas")
                .HasNoKey();
        }
    }
}