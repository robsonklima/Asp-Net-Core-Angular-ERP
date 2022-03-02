using SAT.MODELS.ViewModels;
using SAT.MODELS.ViewModels.Dashboard;

namespace SAT.SERVICES.Interfaces
{
    public interface IDashboardService
    {
        ViewDadosDashboard ObterViewPorParametros(ViewDadosDashboardParameters viewDadosDashboardParameters);
    }
}
