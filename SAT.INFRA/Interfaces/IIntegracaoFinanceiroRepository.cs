using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IIntegracaoFinanceiroRepository
    {
        IEnumerable<ViewIntegracaoFinanceiroOrcamento> ObterOrcamentos(IntegracaoFinanceiroParameters parameters);
        IEnumerable<ViewIntegracaoFinanceiroOrcamentoItem> ObterOrcamentoItens(IntegracaoFinanceiroParameters parameters);
    }
}