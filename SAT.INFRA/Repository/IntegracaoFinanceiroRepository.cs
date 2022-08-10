using SAT.INFRA.Context;
using System;
using SAT.INFRA.Interfaces;
using System.Collections.Generic;
using SAT.MODELS.ViewModels;
using System.Linq;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

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

            if (parameters.DataFechamento != DateTime.MinValue)
                orcamentos = orcamentos.Where(o => o.DataHoraFechamento.Date == parameters.DataFechamento.Date);

            if (parameters.AnoFechamento != DateTime.MinValue)
                orcamentos = orcamentos.Where(o => o.DataHoraFechamento.Year == parameters.AnoFechamento.Year);

            if (parameters.TipoFaturamento != null)
                orcamentos = orcamentos.Where(o => o.TipoFaturamento == (int)parameters.TipoFaturamento);

            return orcamentos;
        }

        public IEnumerable<ViewIntegracaoFinanceiroOrcamentoItem> ObterOrcamentoItens(IntegracaoFinanceiroParameters parameters)
        {
            var itens = _context.ViewIntegracaoFinanceiroOrcamentoItem
                .AsQueryable();

            if (parameters.CodOrc.HasValue)
                itens = itens.Where(i => i.CodOrc == parameters.CodOrc);

            if (parameters.TipoFaturamento != null)
                itens = itens.Where(o => o.TipoFaturamento == (int)parameters.TipoFaturamento);

            itens = itens.OrderBy(i => i.SeqItemPedido);

            return itens;
        }

        public void SalvarRetorno(OrcIntegracaoFinanceiro orcIntegracaoFinanceiro)
        {
            try
            {
                _context.Add(orcIntegracaoFinanceiro);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }
    }
}