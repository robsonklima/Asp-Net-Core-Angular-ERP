using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DefeitoParameters : QueryStringParameters
    {
        public int? CodDefeito { get; set; }
        public int? IndAtivo { get; set; }
    }
}
