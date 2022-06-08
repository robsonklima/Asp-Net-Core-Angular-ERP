using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Params
{
    public class DashboardParameters : QueryStringParameters
    {
        public DashboardViewEnum DashboardViewEnum { get; set; }
        public int? CodPeca { get; set; }
        public int? CodFilial { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public string CodClientes{ get; set; }
    }
}
