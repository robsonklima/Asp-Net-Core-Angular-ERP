using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosReincidenciaClienteMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosReincidenciaCliente>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosReincidenciaCliente> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_reincidencia_cliente")
                .HasNoKey();
        }
    }
}