using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class CausaImprodutividadeParameters : QueryStringParameters
    {
        public int? CodCausaImprodutividade { get; set; }
        public int? CodProtocolo { get; set; }
        public int? CodImprodutividade { get; set; }
        public int? IndAtivo { get; set; }
    }
}