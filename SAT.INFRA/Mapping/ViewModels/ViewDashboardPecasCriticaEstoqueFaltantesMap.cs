using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardPecasCriticaEstoqueFaltantesMap : IEntityTypeConfiguration<ViewDashboardPecasCriticaEstoqueFaltantes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardPecasCriticaEstoqueFaltantes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_top_pecas_faltantes_estoque")
                .HasNoKey();
        }
    }
}