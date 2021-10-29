using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaPeriodoTecnicoParameters : QueryStringParameters
    {
        public int? CodTecnico { get; set; }
        public string CodDespesaPeriodos { get; set; }
        public int? IndAtivoPeriodo { get; set; }
    }
}