using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class LocalEnvioNFFaturamentoVinculadoParameters : QueryStringParameters
    {
        public int CodLocalEnvioNFFaturamento { get; set; }
        public int CodPosto { get; set; }
        public int CodContrato { get; set; }
    }
}
