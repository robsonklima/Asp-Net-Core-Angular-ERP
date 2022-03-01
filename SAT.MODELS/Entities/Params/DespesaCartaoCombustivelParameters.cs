using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DespesaCartaoCombustivelParameters : QueryStringParameters
    {
        public int? CodDespesaCartaoCombustivel { get; set; }
        public int? IndAtivo { get; set; }
    }
}
