using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosProdutividadeMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosProdutividade>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosProdutividade> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_produtividade")
                .HasNoKey();
        }
    }
}