using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosSLAClienteMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosSLACliente>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosSLACliente> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_sla_cliente")
                .HasNoKey();
        }
    }
}