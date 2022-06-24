using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using SAT.INFRA.Interfaces;

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
            var query = _context.TicketLogPedidoCredito.AsQueryable();

            if (parameters.CodDespesaPeriodoTecnico != null)
            {
                query = query.Where(a => a.CodDespesaPeriodoTecnico == parameters.CodDespesaPeriodoTecnico);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<TicketLogPedidoCredito>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}