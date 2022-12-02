using System.Collections.Generic;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Interfaces
{
    public interface IDashboardLabService
    {
        List<ViewDashboardLabRecebidosReparados> ObterRecebidosReparados(DashboardLabParameters parameters);
    }
}