using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Interfaces
{
    public interface IDashboardService
    {
        ViewDadosDashboard ObterViewPorParametros(DashboardParameters parameters);
    }
}
