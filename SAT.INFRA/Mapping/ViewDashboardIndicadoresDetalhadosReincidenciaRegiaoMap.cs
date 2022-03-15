using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardIndicadoresDetalhadosReincidenciaRegiaoMap : IEntityTypeConfiguration<ViewDashboardIndicadoresDetalhadosReincidenciaRegiao>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardIndicadoresDetalhadosReincidenciaRegiao> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_indicadores_detalhados_reincidencia_regiao")
                .HasNoKey();
        }
    }
}