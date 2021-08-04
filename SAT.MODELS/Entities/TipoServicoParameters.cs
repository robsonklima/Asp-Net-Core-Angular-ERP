using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class TipoServicoParameters : QueryStringParameters
    {
        public int? CodServico { get; set; }
        public int? IndAtivo { get; set; }
    }
}
