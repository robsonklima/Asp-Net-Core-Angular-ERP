using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardTecnicosMaisPendentesMap : IEntityTypeConfiguration<ViewDashboardTecnicosMaisPendentes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardTecnicosMaisPendentes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_pendencia_filiais_tecnicos_mais_pendentes")
                .HasNoKey();
        }
    }
}