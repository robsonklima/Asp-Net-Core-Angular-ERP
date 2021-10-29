using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaPeriodoTecnicoParameters : QueryStringParameters
    {
        public string CodTecnicos { get; set; }
        public string CodDespesaPeriodos { get; set; }
        public int? IndAtivoPeriodo { get; set; }
    }
}