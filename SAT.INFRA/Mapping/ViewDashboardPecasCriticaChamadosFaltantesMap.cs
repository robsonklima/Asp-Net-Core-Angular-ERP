using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardPecasCriticaChamadosFaltantesMap : IEntityTypeConfiguration<ViewDashboardPecasCriticaChamadosFaltantes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardPecasCriticaChamadosFaltantes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_top_pecas_faltantes_chamados_faltantes")
                .HasNoKey();
        }
    }
}