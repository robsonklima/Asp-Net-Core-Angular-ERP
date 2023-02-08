using System.Collections.Generic;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces
{
    public interface IDashboardLabRepository
    {
        List<ViewDashboardLabRecebidosReparados> ObterRecebidosReparados(DashboardLabParameters parameters);
        List<ViewDashboardLabTopFaltantes> ObterTopFaltantes(DashboardLabParameters parameters);
        List<ViewDashboardLabTopTempoMedioReparo> ObterTempoMedioReparo(DashboardLabParameters parameters);
        List<ViewDashboardLabProdutividadeTecnica> ObterProdutividadeTecnica(DashboardLabParameters parameters);
        List<ViewDashboardLabTopItensMaisAntigos> ObterTopItensMaisAntigos(DashboardLabParameters parameters);
    }
}
