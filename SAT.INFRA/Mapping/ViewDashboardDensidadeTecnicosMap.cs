using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardDensidadeTecnicosMap : IEntityTypeConfiguration<ViewDashboardDensidadeTecnicos>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardDensidadeTecnicos> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_mapa_densidade_tecnicos")
                .HasNoKey();
        }
    }
}