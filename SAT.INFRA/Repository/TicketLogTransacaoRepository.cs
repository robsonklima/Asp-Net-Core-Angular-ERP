using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using System;

namespace SAT.INFRA.Repository
{
    public partial class TicketLogTransacaoRepository : ITicketLogTransacaoRepository
    {
        private readonly AppDbContext _context;
        public TicketLogTransacaoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public PagedList<TicketLogTransacao> ObterPorParametros(TicketLogTransacaoParameters parameters)
        {
            var query = _context.TicketLogTransacao.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                query = query.Where(
                    s =>
                    s.Uf.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.NomeCidade.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.Responsavel.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.DataInicio != DateTime.MinValue)
                query = query.Where(t => t.DataTransacao.Date >= parameters.DataInicio.Date);

            if (parameters.DataFim != DateTime.MinValue)
                query = query.Where(t => t.DataTransacao.Date <= parameters.DataFim.Date);

            if (!string.IsNullOrWhiteSpace(parameters.Uf))
                query = query.Where(t => t.Uf.Contains(parameters.Uf));

            if (!string.IsNullOrWhiteSpace(parameters.NumeroCartao))
                query = query.Where(t => t.NumeroCartao.Contains(parameters.NumeroCartao));

            if (!string.IsNullOrWhiteSpace(parameters.Responsavel))
                query = query.Where(t => t.Responsavel.Contains(parameters.Responsavel));

            if (!string.IsNullOrWhiteSpace(parameters.Cidade))
                query = query.Where(t => t.NomeCidade.Contains(parameters.Cidade));

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<TicketLogTransacao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
