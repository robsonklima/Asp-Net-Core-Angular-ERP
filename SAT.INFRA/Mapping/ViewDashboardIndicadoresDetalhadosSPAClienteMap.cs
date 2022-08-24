using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosSPAClienteMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosSPACliente>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosSPACliente> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_spa_cliente")
                .HasNoKey();
        }
    }
}