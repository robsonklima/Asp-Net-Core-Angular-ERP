using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DespesaAdiantamentoParameters : QueryStringParameters
    {
        public string CodTecnicos { get; set; }
        public int? IndAtivo { get; set; }
        public string CodDespesaAdiantamentoTipo { get; set; }
    }
}