using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardTecnicosMenosPendentesMap : IEntityTypeConfiguration<ViewDashboardTecnicosMenosPendentes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardTecnicosMenosPendentes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_pendencia_filiais_tecnicos_menos_pendentes")
                .HasNoKey();
        }
    }
}