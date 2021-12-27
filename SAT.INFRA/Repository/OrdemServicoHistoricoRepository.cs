using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class OrdemServicoHistoricoRepository : IOrdemServicoHistoricoRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoHistoricoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<OrdemServicoHistorico> ObterPorParametros(OrdemServicoHistoricoParameters parameters)
        {
            var notificacaoes = _context.OrdemServicoHistorico.AsQueryable();

            if (parameters.CodOS != null)
            {
                notificacaoes = notificacaoes.Where(n => n.CodOS == parameters.CodOS);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                notificacaoes = notificacaoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<OrdemServicoHistorico>.ToPagedList(notificacaoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
