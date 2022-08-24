using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewDespesaImpressaoItemMap : IEntityTypeConfiguration<ViewDespesaImpressaoItem>
    {
        public void Configure(EntityTypeBuilder<ViewDespesaImpressaoItem> builder)
        {
            builder
                .ToView("vwc_v2_despesa_impressao_itens")
                .HasNoKey();
        }
    }
}