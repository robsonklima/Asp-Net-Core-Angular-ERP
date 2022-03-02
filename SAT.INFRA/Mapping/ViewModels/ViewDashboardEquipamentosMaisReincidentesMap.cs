using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardEquipamentosMaisReincidentesMap : IEntityTypeConfiguration<ViewDashboardEquipamentosMaisReincidentes>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardEquipamentosMaisReincidentes> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_reincidencia_equipamentos_mais_reincidentes")
                .HasNoKey();
        }
    }
}