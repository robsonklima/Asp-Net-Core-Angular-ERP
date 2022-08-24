using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardSPATecnicosMaiorDesempenhoMap : IEntityTypeConfiguration<ViewDashboardSPATecnicosMaiorDesempenho>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardSPATecnicosMaiorDesempenho> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_spa_tecnicos_maior_desempenho")
                .HasNoKey();
        }
    }
}