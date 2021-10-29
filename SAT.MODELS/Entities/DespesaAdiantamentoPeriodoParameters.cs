using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoPeriodoParameters : QueryStringParameters
    {
        public string CodDespesaPeriodos { get; set; }
        public int? CodTecnico { get; set; }
        public int? IndAtivoPeriodo { get; set; }
    }
}