using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardReincidenciaClientesMap : IEntityTypeConfiguration<ViewDashboardReincidenciaClientes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardReincidenciaClientes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_reincidencia_clientes")
                .HasNoKey();
        }
    }
}