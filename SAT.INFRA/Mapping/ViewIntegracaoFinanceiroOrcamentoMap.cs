using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewIntegracaoFinanceiroOrcamentoMap : IEntityTypeConfiguration<ViewIntegracaoFinanceiroOrcamento>
    {
        public void Configure(EntityTypeBuilder<ViewIntegracaoFinanceiroOrcamento> builder)
        {
            builder
                .ToView("vwc_v2_integracao_financeiro_orcamento")
                .HasNoKey();
        }
    }
}