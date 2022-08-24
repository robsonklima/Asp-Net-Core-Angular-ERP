using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosPendenciaRegiaoMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosPendenciaRegiao>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosPendenciaRegiao> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_pendencia_regiao")
                .HasNoKey();
        }
    }
}