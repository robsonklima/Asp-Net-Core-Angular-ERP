using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.ViewModels.Dashboard
{
    public class ViewDadosDashboardParameters : QueryStringParameters
    {
        public DashboardViewEnum DashboardViewEnum { get; set; }
        public int? CodPeca { get; set; }
        public int? CodFilial { get; set; }
    }
}
