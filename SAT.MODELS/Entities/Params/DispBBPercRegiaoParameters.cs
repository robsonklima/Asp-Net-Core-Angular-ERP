using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class DispBBPercRegiaoParameters : QueryStringParameters
    {
        public int? CodDispBBRegiao { get; set; }
        public int? Criticidade { get; set; }
        public int? IndAtivo { get; set; }
    }
}