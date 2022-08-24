using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IIntegracaoFinanceiroRepository
    {
        IEnumerable<ViewIntegracaoFinanceiroOrcamento> ObterOrcamentos(IntegracaoFinanceiroParameters parameters);
        IEnumerable<ViewIntegracaoFinanceiroOrcamentoItem> ObterOrcamentoItens(IntegracaoFinanceiroParameters parameters);
        void SalvarRetorno(OrcIntegracaoFinanceiro orcIntegracaoFinanceiro);
    }
}