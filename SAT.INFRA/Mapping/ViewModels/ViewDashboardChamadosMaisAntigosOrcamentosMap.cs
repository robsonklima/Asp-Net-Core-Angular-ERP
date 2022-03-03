using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardChamadosMaisAntigosOrcamentosMap : IEntityTypeConfiguration<ViewDashboardChamadosMaisAntigosOrcamentos>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardChamadosMaisAntigosOrcamentos> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_chamados_antigos_orcamentos")
                .HasNoKey();
        }
    }
}