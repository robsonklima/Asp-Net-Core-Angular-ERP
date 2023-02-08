using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DashboardLabService : IDashboardLabService
    {
        private readonly IDashboardLabRepository _dashboardLabRepo;

        public DashboardLabService(IDashboardLabRepository dashboardLabRepo)
        {
            _dashboardLabRepo = dashboardLabRepo;
        }

        public List<ViewDashboardLabRecebidosReparados> ObterRecebidosReparados(DashboardLabParameters parameters)
        {
            return _dashboardLabRepo.ObterRecebidosReparados(parameters);
        }

        public List<ViewDashboardLabTopFaltantes> ObterTopFaltantes(DashboardLabParameters parameters)
        {
            return _dashboardLabRepo.ObterTopFaltantes(parameters);
        }

        public List<ViewDashboardLabTopTempoMedioReparo> ObterTempoMedioReparo(DashboardLabParameters parameters)
        {
            return _dashboardLabRepo.ObterTempoMedioReparo(parameters);
        }

        public List<ViewDashboardLabProdutividadeTecnica> ObterProdutividadeTecnica(DashboardLabParameters parameters)
        {
            return _dashboardLabRepo.ObterProdutividadeTecnica(parameters);
        }
    }
}
