using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardSPATecnicosMenorDesempenhoMap : IEntityTypeConfiguration<ViewDashboardSPATecnicosMenorDesempenho>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardSPATecnicosMenorDesempenho> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_spa_tecnicos_menor_desempenho")
                .HasNoKey();
        }
    }
}