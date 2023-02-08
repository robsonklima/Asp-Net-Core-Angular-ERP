using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Views;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class DashboardLabRepository : IDashboardLabRepository
    {
        private readonly AppDbContext _context;

        public DashboardLabRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ViewDashboardLabRecebidosReparados> ObterRecebidosReparados(DashboardLabParameters parameters)
        {
            var query = _context.ViewDashboardLabRecebidosReparados.AsQueryable();

            if (parameters.Ano.HasValue)
                query = query.Where(q => q.Ano == parameters.Ano);

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return query.ToList();
        }

        public List<ViewDashboardLabTopFaltantes> ObterTopFaltantes(DashboardLabParameters parameters)
        {
            var query = _context.ViewDashboardLabTopFaltantes.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return query.ToList();
        }

        public List<ViewDashboardLabTopTempoMedioReparo> ObterTempoMedioReparo(DashboardLabParameters parameters)
        {
            var query = _context.ViewDashboardLabTopTempoMedioReparo.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return query.ToList();
        }

        public List<ViewDashboardLabProdutividadeTecnica> ObterProdutividadeTecnica(DashboardLabParameters parameters)
        {
            var query = _context.ViewDashboardLabProdutividadeTecnica.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return query.ToList();
        }

        public List<ViewDashboardLabTopItensMaisAntigos> ObterTopItensMaisAntigos(DashboardLabParameters parameters)
        {
            var query = _context.ViewDashboardLabTopItensMaisAntigos.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return query.ToList();
        }

        public List<ViewDashboardLabIndiceReincidencia> ObterIndiceReincidencia(DashboardLabParameters parameters)
        {
            var query = _context.ViewDashboardLabIndiceReincidencia.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return query.ToList();
        }
    }
}
