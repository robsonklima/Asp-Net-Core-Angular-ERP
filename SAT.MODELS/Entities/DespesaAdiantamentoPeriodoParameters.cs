using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoPeriodoParameters : QueryStringParameters
    {
        public int? CodDespesaPeriodo { get; set; }
        public int? CodTecnico { get; set; }
        public int? IndAtivoPeriodo { get; set; }
    }
}