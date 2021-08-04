using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class AcaoParameters : QueryStringParameters
    {
        public int? CodAcao { get; set; }
        public int? IndAtivo { get; set; }
    }
}
