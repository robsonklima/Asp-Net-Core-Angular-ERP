using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardReincidenciaTecnicosMenosReincidentesMap : IEntityTypeConfiguration<ViewDashboardReincidenciaTecnicosMenosReincidentes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardReincidenciaTecnicosMenosReincidentes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_reincidencia_filiais_tecnicos_menos_reincidentes")
                .HasNoKey();
        }
    }
}