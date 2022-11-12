using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ORSolucaoParameters : QueryStringParameters
    {
        public int? CodSolucao { get; set; }
        public int? IndAtivo { get; set; }
    }
}
