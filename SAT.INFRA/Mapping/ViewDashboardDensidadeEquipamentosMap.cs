using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardDensidadeEquipamentosMap : IEntityTypeConfiguration<ViewDashboardDensidadeEquipamentos>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardDensidadeEquipamentos> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_mapa_densidade_equipamentos")
                .HasNoKey();
        }
    }
}