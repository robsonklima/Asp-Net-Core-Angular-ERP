using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosSLARegiaoMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosSLARegiao>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosSLARegiao> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_sla_regiao")
                .HasNoKey();
        }
    }
}