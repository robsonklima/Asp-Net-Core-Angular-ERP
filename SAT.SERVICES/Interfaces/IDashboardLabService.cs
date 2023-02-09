using System.Collections.Generic;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Interfaces
{
    public interface IDashboardLabService
    {
        List<ViewDashboardLabRecebidosReparados> ObterRecebidosReparados(DashboardLabParameters parameters);
        List<ViewDashboardLabTopFaltantes> ObterTopFaltantes(DashboardLabParameters parameters);
        List<ViewDashboardLabTopTempoMedioReparo> ObterTempoMedioReparo(DashboardLabParameters parameters);
        List<ViewDashboardLabProdutividadeTecnica> ObterProdutividadeTecnica(DashboardLabParameters parameters);
        List<ViewDashboardLabTopItensMaisAntigos> ObterTopItensMaisAntigos(DashboardLabParameters parameters);
        List<ViewDashboardLabIndiceReincidencia> ObterIndiceReincidencia(DashboardLabParameters parameters);
    }
}
