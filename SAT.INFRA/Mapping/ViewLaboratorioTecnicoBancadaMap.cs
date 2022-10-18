using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewLaboratorioTecnicoBancadaMap : IEntityTypeConfiguration<ViewLaboratorioTecnicoBancada>
    {
        public void Configure(EntityTypeBuilder<ViewLaboratorioTecnicoBancada> builder)
        {
            builder
                .ToView("vwc_v2_laboratorio_tecnicos_bancada")
                .HasNoKey();
        }
    }
}