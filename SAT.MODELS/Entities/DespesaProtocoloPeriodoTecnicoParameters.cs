using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaProtocoloPeriodoTecnicoParameters : QueryStringParameters
    {
        public int? IndAtivo { get; set; }
        public int? CodDespesaPeriodoTecnico { get; set; }
    }
}