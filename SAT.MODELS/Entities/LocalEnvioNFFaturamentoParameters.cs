using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class LocalEnvioNFFaturamentoParameters : QueryStringParameters
    {
        public int CodLocalEnvioNFFaturamento { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
    }
}
