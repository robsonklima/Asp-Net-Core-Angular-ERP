using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardPecasMaisFaltantesMap : IEntityTypeConfiguration<ViewDashboardPecasMaisFaltantes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardPecasMaisFaltantes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_pecas_faltantes_5_pecas_mais_faltantes")
                .HasNoKey();
        }
    }
}