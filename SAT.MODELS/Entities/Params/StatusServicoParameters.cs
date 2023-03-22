using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class StatusServicoParameters : QueryStringParameters
    {
        public int? CodStatusServico { get; set; }
        public int? IndAtivo { get; set; }
        public string CodStatusServicos { get; set; }
    }
}
