using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ImprodutividadeParameters : QueryStringParameters
    {
        public int? CodImprodutividade { get; set; }
        public int? IndAtivo { get; set; }
    }
}