using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardChamadosMaisAntigosCorretivasMap : IEntityTypeConfiguration<ViewDashboardChamadosMaisAntigosCorretivas>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardChamadosMaisAntigosCorretivas> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_chamados_antigos_corretivas")
                .HasNoKey();
        }
    }
}