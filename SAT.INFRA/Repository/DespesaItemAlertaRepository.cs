using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class DespesaItemAlertaRepository : IDespesaItemAlertaRepository
    {
        private readonly AppDbContext _context;

        public DespesaItemAlertaRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<DespesaItemAlerta> ObterPorParametros(DespesaItemAlertaParameters parameters)
        {
            var alertas =
                _context.DespesaItemAlerta.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                alertas = alertas.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaItemAlerta>.ToPagedList(alertas, parameters.PageNumber, parameters.PageSize);
        }
    }
}