using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewDashboardDisponibilidadeTecnicosMap : IEntityTypeConfiguration<ViewDashboardDisponibilidadeTecnicos>
    {
        public void Configure(EntityTypeBuilder<ViewDashboardDisponibilidadeTecnicos> builder)
        {
            builder
                .ToView("vwc_v2_dashboard_disponibilidade_tecnicos")
                .HasNoKey();
        }
    }
}