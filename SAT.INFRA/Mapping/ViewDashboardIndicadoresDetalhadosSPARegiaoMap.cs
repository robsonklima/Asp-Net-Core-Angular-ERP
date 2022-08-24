using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosSPARegiaoMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosSPARegiao>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosSPARegiao> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_spa_regiao")
                .HasNoKey();
        }
    }
}