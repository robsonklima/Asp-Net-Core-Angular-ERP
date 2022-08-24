using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardReincidenciaTecnicosMaisReincidentesMap : IEntityTypeConfiguration<ViewDashboardReincidenciaTecnicosMaisReincidentes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardReincidenciaTecnicosMaisReincidentes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_reincidencia_filiais_tecnicos_mais_reincidentes")
                .HasNoKey();
        }
    }
}