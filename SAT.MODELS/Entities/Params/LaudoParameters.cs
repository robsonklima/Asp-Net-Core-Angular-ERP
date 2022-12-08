using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class LaudoParameters : QueryStringParameters
    {
        public int? CodLaudo { get; set; }
        public int? CodOS { get; set; }
        public int? CodRAT { get; set; }
        public int? CodTecnico { get; set; }
        public int? IndAtivo { get; set; }
        public string CodClientes { get; set; }
        public string CodEquips { get; set; }
        public string CodTecnicos { get; set; }
        public string CodLaudoStatus { get; set; }
    }
}