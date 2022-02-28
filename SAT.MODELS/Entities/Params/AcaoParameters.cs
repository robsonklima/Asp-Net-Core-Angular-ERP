using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class AcaoParameters : QueryStringParameters
    {
        public int? CodAcao { get; set; }
        public int? IndAtivo { get; set; }
    }
}
