using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
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
            {
                query = query.Where(q => q.Ano == parameters.Ano);
            }

            return query.ToList();
        }
    }
}
