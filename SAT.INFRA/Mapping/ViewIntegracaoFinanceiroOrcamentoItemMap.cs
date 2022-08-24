using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewIntegracaoFinanceiroOrcamentoItemMap : IEntityTypeConfiguration<ViewIntegracaoFinanceiroOrcamentoItem>
    {
        public void Configure(EntityTypeBuilder<ViewIntegracaoFinanceiroOrcamentoItem> builder)
        {
            builder
                .ToView("vwc_v2_integracao_financeiro_orcamento_item")
                .HasNoKey();
        }
    }
}