using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class DashboardParameters : QueryStringParameters
    {
        public DashboardViewEnum DashboardViewEnum { get; set; }
        public int? CodPeca { get; set; }
        public int? CodFilial { get; set; }
    }
}
