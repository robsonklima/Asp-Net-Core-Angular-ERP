using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ContratoServicoParameters : QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public int? CodContratoServico{ get; set; }
    }
}
