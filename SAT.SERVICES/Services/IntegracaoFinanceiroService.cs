using System;
using System.Collections.Generic;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class IntegracaoFinanceiroService : IIntegracaoFinanceiroService
    {
        private IIntegracaoFinanceiroRepository _integracaoFinanceiroRepo;

        public IntegracaoFinanceiroService(
            IIntegracaoFinanceiroRepository integracaoFinanceiroRepo
        ) {
            _integracaoFinanceiroRepo = integracaoFinanceiroRepo;
        }

        public void Executar()
        {
            var orcamentos = ObterOrcamentos();
            var itens = ObterItens(orcamentos);
        }

        private List<ViewIntegracaoFinanceiroOrcamento> ObterOrcamentos() {
            var orcamentos = _integracaoFinanceiroRepo
                .ObterOrcamentos(new IntegracaoFinanceiroParameters { 
                    CodStatusServico = (int)StatusServicoEnum.FECHADO,
                    CodTipoIntervencao = (int)TipoIntervencaoEnum.ORC_APROVADO,
                    DataFechamento = DateTime.Now.AddDays(-5)
                })
                .ToList();

            return orcamentos;
        }

        private List<ViewIntegracaoFinanceiroOrcamentoItem> ObterItens(List<ViewIntegracaoFinanceiroOrcamento> orcamentos) {
            List<ViewIntegracaoFinanceiroOrcamentoItem> itens = new();

            foreach (var orc in orcamentos)
            {
                itens.AddRange(_integracaoFinanceiroRepo.ObterOrcamentoItens(new IntegracaoFinanceiroParameters { 
                    CodOrc = orc.CodOrc,
                    TipoFaturamento = TipoFaturamentoOrcEnum.PRODUTO
                }).ToList());
            }

            return itens;
        }
    }
}
