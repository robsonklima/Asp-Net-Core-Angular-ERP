using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoParameters : QueryStringParameters
    {
        public int? CodTecnico { get; set; }
        public int? CodDespesaAdiantamento { get; set; }
    }
}