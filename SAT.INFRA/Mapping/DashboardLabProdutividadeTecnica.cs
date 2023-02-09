using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class DashboardLabProdutividadeTecnicaMap : IEntityTypeConfiguration<ViewDashboardLabProdutividadeTecnica>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardLabProdutividadeTecnica> builder)
        {
            builder
                .ToView("vwc_v2_laboratorio_dashboard_produtividade_tecnica")
                .HasNoKey();
        }
    }
}