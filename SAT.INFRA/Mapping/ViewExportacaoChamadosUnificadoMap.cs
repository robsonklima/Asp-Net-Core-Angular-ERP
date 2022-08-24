using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewExportacaoChamadosUnificadoMap : IEntityTypeConfiguration<ViewExportacaoChamadosUnificado>
    {
        public void Configure(EntityTypeBuilder<ViewExportacaoChamadosUnificado> builder)
        {
            builder
                .ToView("vwc_v2_exportacao_chamados_unificado")
                .HasNoKey();
        }
    }
}