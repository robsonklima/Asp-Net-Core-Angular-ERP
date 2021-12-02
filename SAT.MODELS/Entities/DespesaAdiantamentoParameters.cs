using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoParameters : QueryStringParameters
    {
        public string CodTecnicos { get; set; }
        public int? IndAtivo { get; set; }
        public string CodDespesaAdiantamentoTipo { get; set; }
    }
}