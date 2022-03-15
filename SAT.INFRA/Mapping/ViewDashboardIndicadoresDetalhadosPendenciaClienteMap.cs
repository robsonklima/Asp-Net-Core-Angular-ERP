using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosPendenciaClienteMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosPendenciaCliente>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosPendenciaCliente> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_pendencia_cliente")
                .HasNoKey();
        }
    }
}