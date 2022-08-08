using SAT.INFRA.Context;
using System;
using SAT.INFRA.Interfaces;
using System.Collections.Generic;
using SAT.MODELS.ViewModels;
using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Repository
{
    public class IntegracaoFinanceiroRepository : IIntegracaoFinanceiroRepository
    {
        private readonly AppDbContext _context;

        public IntegracaoFinanceiroRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ViewIntegracaoFinanceiroOrcamento> ObterOrcamentos(IntegracaoFinanceiroParameters parameters)
        {
            var orcamentos = _context.ViewIntegracaoFinanceiroOrcamento
                .AsQueryable();

            if (parameters.CodOrc.HasValue)
                orcamentos = orcamentos.Where(o => o.CodOrc == parameters.CodOrc);

            if (parameters.CodStatusServico.HasValue)
                orcamentos = orcamentos.Where(o => o.CodStatusServico == parameters.CodStatusServico);

            if (parameters.CodTipoIntervencao.HasValue)
                orcamentos = orcamentos.Where(o => o.CodTipoIntervencao == parameters.CodTipoIntervencao);

            return orcamentos;
        }

        public IEnumerable<ViewIntegracaoFinanceiroOrcamentoItem> ObterOrcamentosItens(IntegracaoFinanceiroParameters parameters)
        {
            var itens = _context.ViewIntegracaoFinanceiroOrcamentoItem
                .AsQueryable();

            if (parameters.CodOrc.HasValue)
                itens = itens.Where(i => i.CodOrc == parameters.CodOrc);

            itens = itens.Where(i => i.TipoFaturamento == (int)parameters.TipoFaturamento);
            itens = itens.OrderBy(i => i.SeqItemPedido);

            return itens;
        }
    }
}