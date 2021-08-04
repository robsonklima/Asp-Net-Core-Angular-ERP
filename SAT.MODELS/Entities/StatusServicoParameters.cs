using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class StatusServicoParameters : QueryStringParameters
    {
        public int? CodStatusServico { get; set; }
        public int? IndAtivo { get; set; }
    }
}
