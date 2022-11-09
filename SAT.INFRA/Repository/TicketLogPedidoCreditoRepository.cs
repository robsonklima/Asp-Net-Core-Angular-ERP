using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using System.Linq.Dynamic.Core;
using SAT.INFRA.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class TicketLogPedidoCreditoRepository : ITicketLogPedidoCreditoRepository
    {
        private readonly AppDbContext _context;

        public TicketLogPedidoCreditoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(TicketLogPedidoCredito pedidoCredito)
        {
            _context.Add(pedidoCredito);
            _context.SaveChanges();
        }

        public PagedList<TicketLogPedidoCredito> ObterPorParametros(TicketLogPedidoCreditoParameters parameters)
        {
            var query = _context.TicketLogPedidoCredito
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    t =>
                    t.CodTicketLogPedidoCredito.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.CodDespesaPeriodoTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Valor.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.NumeroCartao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodDespesaPeriodoTecnico != null)
                query = query.Where(a => a.CodDespesaPeriodoTecnico == parameters.CodDespesaPeriodoTecnico);

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<TicketLogPedidoCredito>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public void Deletar(int cod)
        {
            TicketLogPedidoCredito t = _context.TicketLogPedidoCredito.FirstOrDefault(t => t.CodTicketLogPedidoCredito == cod);

            if (t != null)
            {
                _context.TicketLogPedidoCredito.Remove(t);
                _context.SaveChanges();
            }
        }
    }
}