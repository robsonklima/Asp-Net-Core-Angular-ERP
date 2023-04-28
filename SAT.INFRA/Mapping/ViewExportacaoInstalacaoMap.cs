using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewExportacaoInstalacaoMap : IEntityTypeConfiguration<ViewExportacaoInstalacao>
    {
        public void Configure(EntityTypeBuilder<ViewExportacaoInstalacao> builder)
        {
            builder
                .ToView("vwc_v2_instalacao_faturamento")
                .HasNoKey();
        }
    }
}