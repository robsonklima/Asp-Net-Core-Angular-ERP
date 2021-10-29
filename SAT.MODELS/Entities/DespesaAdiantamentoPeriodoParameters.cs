using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoPeriodoParameters : QueryStringParameters
    {
        public string CodDespesaPeriodos { get; set; }
        public string CodTecnicos { get; set; }
        public int? IndAtivoPeriodo { get; set; }
    }
}