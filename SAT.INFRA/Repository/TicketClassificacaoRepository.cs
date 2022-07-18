using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TicketClassificacaoRepository : ITicketClassificacaoRepository
    {
        private readonly AppDbContext _context;

        public TicketClassificacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<TicketClassificacao> ObterPorParametros(TicketClassificacaoParameters parameters)
        {
            var query = _context
                            .TicketClassificacao
                                .AsQueryable();

            // if (parameters.CodPrioridade != null)
            // {
            //     query = query.Where(t => t.CodPrioridade == parameters.CodPrioridade);
            // }

            return PagedList<TicketClassificacao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        public TicketClassificacao ObterPorCodigo(int CodClassificacao)
        {
            return _context.TicketClassificacao
            .FirstOrDefault(t => t.CodClassificacao == CodClassificacao);
        }

        public void Atualizar(TicketClassificacao ticketClassificacao)
        {

            _context.ChangeTracker.Clear();
            TicketClassificacao tick = _context.TicketClassificacao.SingleOrDefault(t => t.CodClassificacao == ticketClassificacao.CodClassificacao);

            if (tick != null)
            {
                try
                {
                    _context.Entry(tick).CurrentValues.SetValues(ticketClassificacao);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }
    }
}