using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardSLAClientesMap : IEntityTypeConfiguration<ViewDashboardSLAClientes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardSLAClientes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_sla_clientes")
                .HasNoKey();
        }
    }
}