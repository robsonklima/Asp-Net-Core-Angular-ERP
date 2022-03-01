using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DespesaProtocoloPeriodoTecnicoParameters : QueryStringParameters
    {
        public int? IndAtivo { get; set; }
        public int? CodDespesaPeriodoTecnico { get; set; }
    }
}