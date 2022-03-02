using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardPecasFaltantesMap : IEntityTypeConfiguration<ViewDashboardPecasFaltantes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardPecasFaltantes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_pecas_faltantes_filiais")
                .HasNoKey();
        }
    }
}