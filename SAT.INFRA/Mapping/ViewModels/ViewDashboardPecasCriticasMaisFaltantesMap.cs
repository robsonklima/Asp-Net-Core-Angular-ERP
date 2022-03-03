using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardPecasCriticasMaisFaltantesMap : IEntityTypeConfiguration<ViewDashboardPecasCriticasMaisFaltantes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardPecasCriticasMaisFaltantes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_top_pecas_faltantes")
                .HasNoKey();
        }
    }
}